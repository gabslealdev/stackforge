using Bogus;
using Shouldly;
using StackForge.Domain.Profile.ValueObjects;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Tests.Profile.ValueObjects
{
    public class NameUnitTest
    {
        private readonly Faker _faker = new();

        [Fact]
        public void ShouldCreate_WhenValueIsValid()
        {
            // arrange
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Name.LastName();

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ShouldThrow_WhenFirstNameIsNullOrWhiteSpace()
        {
            // arrange
            var firstName = string.Empty;
            var lastName = _faker.Name.LastName();

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.FirstName.Required");
            exception.Error.Message.ShouldBe("First name is required.");

        }

        [Fact]
        public void ShouldThrow_WhenFirstNameIsTooShort()
        {
            // arrange
            var firstName = _faker.Random.String2(2);
            var lastName = _faker.Name.LastName();

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.FirstName.TooShort");
            exception.Error.Message.ShouldBe("First name must be at least 3 characters long.");
        }

        [Fact]
        public void ShouldThrow_WhenFirstNameIsTooLong()
        {
            // arrange
            var firstName = _faker.Random.String2(81);
            var lastName = _faker.Name.LastName();

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.FirstName.TooLong");
            exception.Error.Message.ShouldBe("First name must be at most 80 characters long.");
        }

        [Fact]
        public void ShouldThrow_WhenLastNameIsNullOrWhiteSpace()
        {
            // arrange
            var firstName = _faker.Name.FirstName();
            var lastName = string.Empty;

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.LastName.Required");
            exception.Error.Message.ShouldBe("Last name is required.");
        }

        [Fact]
        public void ShouldThrow_WhenLasNameIsTooShort()
        {
            // arrange
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Random.String2(2);


            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.LastName.TooShort");
            exception.Error.Message.ShouldBe("Last name must be at least 3 characters long.");
        }

        [Fact]
        public void ShouldThrow_WhenLastNameIsTooLong()
        {
            // arrange
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Random.String2(81);

            // act
            Action action = () => Name.Create(firstName, lastName);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Profile.LastName.TooLong");
            exception.Error.Message.ShouldBe("Last name must be at most 80 characters long.");

        }

        [Fact]
        public void ToString_ShouldReturnFullName()
        {
            // arrange
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Name.LastName();
            var fullName = $"{firstName} {lastName}";
            var name = Name.Create(firstName, lastName);

            // act
            var result = name.ToString();

            // assert
            result.ShouldBe(fullName);

        }
    }
}
