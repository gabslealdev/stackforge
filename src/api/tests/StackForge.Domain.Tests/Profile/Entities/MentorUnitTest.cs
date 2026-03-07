using Bogus;
using Shouldly;
using StackForge.Domain.Profile.Enum;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Tests.Profile.Builders;
using StackForge.Domain.Tests.Stacks.Builders;

namespace StackForge.Domain.Tests.Profile.Entities
{
    public class MentorUnitTest
    {
        private readonly Faker _faker = new();

        [Fact]
        public void ShouldCreate_WhenMentorIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder();

            // act 
            Action action = () =>  mentor.Build();

            // assert
            action.ShouldNotThrow();
            
        }

        [Fact]
        public void ShouldThrow_WhenUserIdIsEmpty()
        {
            // arrange
            var mentor = new MentorProfileBuilder().WithUserId(Guid.Empty);

            // act 
            Action action = () => mentor.Build();

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.UserId.Required");
            exception.Error.Message.ShouldBe("User is required.");
        }

        [Fact]
        public void ShouldThrow_WhenBithDateIsFuture()
        {
            // arrange
            var mentor = new MentorProfileBuilder().WithBirthDate(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));

            // act 
            Action action = () => mentor.Build();

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.BirthDate.InFuture");
            exception.Error.Message.ShouldBe("Birth date cannot be in the future.");
        }

        [Fact]
        public void ShouldAddStack_WhenStackIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var stack = new StackBuilder().WithName(".Net").WithKey("dotnet").Build();

            // act
            Action action = () => mentor.AddStack(stack);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void AddStack_ShouldThrow_WhenStackAlreadyAdded()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();    
            var stack = new StackBuilder().WithName("Angular 20").WithKey("angular").Build();
            mentor.AddStack(stack);

            // act
            Action action = () => mentor.AddStack(stack);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Mentor.Stack.AlreadyAdded");
            exception.Error.Message.ShouldBe("This stack has already been added to the mentor profile.");
        }

        [Fact]
        public void ShouldRemoveStack_WhenStackIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var stack1 = new StackBuilder().WithName("PostgreSQL").WithKey("postgreSQL").Build();
            var stack2 = new StackBuilder().WithName("MongoDB").WithKey("mongodb").Build();

            mentor.AddStack(stack1);
            mentor.AddStack(stack2);

            // act 
            Action action = () => mentor.RemoveStack(stack1.Id);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void RemoveStack_ShouldThrow_WhenStackIsNull()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var stack1 = new StackBuilder().WithName("React").WithKey("react").Build();
            Guid stack2 = Guid.NewGuid();

            // act
            Action action = () => mentor.RemoveStack(stack2);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Mentor.Stack.NotFound");
            exception.Error.Message.ShouldBe("This stack cannot be found.");
        }

        [Fact]
        public void RemoveStack_ShouldThrow_WhenIsTheOnlyOne()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var stack = new StackBuilder().WithName("GoLang").WithKey("golang").Build();
            mentor.AddStack(stack);

            // act 
            Action action = () => mentor.RemoveStack(stack.Id);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Mentor.Stack.OnlyOne");
            exception.Error.Message.ShouldBe("Your profile must include at least one stack.");
        }

        [Fact]
        public void UpdateEducation_ShouldUpdate_WhenEducationIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var courseName = _faker.Random.String2(15);
            var institution = _faker.Company.CompanyName();
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(_faker.Date.Past(5));

            // act 
            mentor.UpdateEducation(courseName, institution, status, conclusionDate);

            // assert
            mentor.Education.CourseName.ShouldBe(courseName);
            mentor.Education.Institution.ShouldBe(institution);
            mentor.Education.Status.ShouldBe(status);
            mentor.Education.ConclusionDate.ShouldBe(conclusionDate);
        }   

        [Fact] 
        public void UpdateMentorProfile_ShouldUpdate_WhenAttributeIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder().Build();
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Name.LastName();
            var birthDate = DateOnly.FromDateTime(_faker.Date.Past(30, DateTime.UtcNow.AddYears(-32)));
            mentor.UpdateMentorProfile(firstName, lastName, birthDate);

            // act 
            mentor.UpdateMentorProfile(firstName, lastName, birthDate);

            // assert
            mentor.Name.FirstName.ShouldBe(firstName);
            mentor.Name.LastName.ShouldBe(lastName);
            mentor.BirthDate.ShouldBe(birthDate);
        }

        [Fact]
        public void ChangeAvailability_ShouldUpdate_WhenAvailabilityIsValid()
        {
            // arrange
            var availability = AvailabityStatus.Unavailable;
            var mentor = new MentorProfileBuilder().WithAvailability(availability).Build();

            // act 
            mentor.ChangeAvailability();

            // assert
            mentor.Availability.ShouldBe(AvailabityStatus.Available);

        }   

        [Fact]
        public void UpdateBio_ShouldUpdate_WhenBioIsValid()
        {
            // arrange
            var mentor = new MentorProfileBuilder().WithBio(null!).Build();
            var bio = _faker.Lorem.Paragraph();

            // act 
            mentor.UpdateBio(bio);

            // assert
            mentor.Bio?.Value.ShouldBe(bio);
        }

    }
}
