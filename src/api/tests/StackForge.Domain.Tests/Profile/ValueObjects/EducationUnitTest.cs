using Bogus;
using Shouldly;
using StackForge.Domain.Profile.Enums;
using StackForge.Domain.Profile.ValueObjects;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Tests.Profile.ValueObjects
{
    public class EducationUnitTest
    {
        private readonly Faker _faker = new();

        [Fact]
        public void ShouldCreate_WhenValueIsValid()
        {
            // arrange 
            var courseName = _faker.Random.String2(10, 50);
            var institution = _faker.Random.String2(10, 50);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            action.ShouldNotThrow();


        }

        [Fact]
        public void ShouldThrow_WhenCoursNameIsNullOrWhiteSpace()
        {
            // arrange 
            var courseName = string.Empty;
            var institution = _faker.Random.String2(10, 50);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var excecption = action.ShouldThrow<DomainExceptionValidation>();
            excecption.Error.Code.ShouldBe("Education.Course.Required");
            excecption.Error.Message.ShouldBe("Course name is required for each education entry.");
        }

        [Fact]
        public void ShouldThrow_WhenCourseNameIsTooShort()
        {
            // arrange 
            var courseName = _faker.Random.String2(1);
            var institution = _faker.Random.String2(10, 50);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var excecption = action.ShouldThrow<DomainExceptionValidation>();
            excecption.Error.Code.ShouldBe("Education.CourseName.IsTooShort");
            excecption.Error.Message.ShouldBe("Course name must be at least 3 characters long.");
        }

        [Fact]
        public void ShouldThrow_WhenCourseNameIsTooLong()
        {
            // arrange 
            var courseName = _faker.Random.String2(101);
            var institution = _faker.Random.String2(10, 50);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var excecption = action.ShouldThrow<DomainExceptionValidation>();
            excecption.Error.Code.ShouldBe("Education.CourseName.IsTooLong");
            excecption.Error.Message.ShouldBe("Course name must be at most 100 characters long.");


        }

        [Fact]
        public void ShouldThrow_WhenInstitutionIsNullOrWhiteSpace()
        {
            // arrange 
            var courseName = _faker.Random.String2(10, 50);
            var institution = string.Empty;
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var excecption = action.ShouldThrow<DomainExceptionValidation>();
            excecption.Error.Code.ShouldBe("Education.Institution.Required");
            excecption.Error.Message.ShouldBe("Institution is required for each education entry.");



        }

        [Fact]
        public void ShouldThrow_WhenInstitutionIsTooShort()
        {
            // arrange 
            var courseName = _faker.Random.String2(10, 50);
            var institution = _faker.Random.String2(1);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1)); 

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var excecption = action.ShouldThrow<DomainExceptionValidation>();
            excecption.Error.Code.ShouldBe("Education.Institution.IsTooShort");
            excecption.Error.Message.ShouldBe("Institution name must be at least 3 characters long.");


        }

        [Fact]
        public void ShouldThrow_WhenInstitutionIsTooLong()
        {
            // arrange 
            var courseName = _faker.Random.String2(10, 50);
            var institution = _faker.Random.String2(101);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Education.Institution.IsTooLong");
            exception.Error.Message.ShouldBe("Institution name must be at most 100 characters long.");


        }

        [Fact]
        public void ShouldThrow_WhenValidateConclusionIsFuture()
        {
            // arrange 
            var courseName = _faker.Random.String2(10, 50);
            var institution = _faker.Random.String2(10, 50);
            var status = EducationStatus.Completed;
            var conclusionDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(1));

            // act
            Action action = () => Education.Create(courseName, institution, status, conclusionDate);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Education.ConclusionDate.Invalid");
            exception.Error.Message.ShouldBe("Conclusion date must be in the past for each education entry.");

        }

    }
}
 