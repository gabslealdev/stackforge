using Bogus;
using Shouldly;
using StackForge.Domain.ProfileContext.ValueObjects;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Tests.Profile.ValueObjects
{
    public class BioUnitTest
    {
        private readonly Faker _faker = new();
        [Fact]
        public void ShouldCreate_WhenIsValid()
        {
            // arrange
            var textBio = _faker.Lorem.Paragraphs(2);

            // act
            Action action = () => Bio.Create(textBio);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ShouldThrow_WhenIsTooShort()
        {
            // arrange
            var textBio = _faker.Random.String2(8);

            // act
            Action action = () => Bio.Create(textBio);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Mentor.Bio.TooShort");
            exception.Error.Message.ShouldBe("The bio must be at least 10 characters long.");
        }

        [Fact]
        public void ShouldThrow_WhenIsTooLong()
        {
            // arrange
            var textBio = _faker.Random.String2(600);

            // act
            Action action = () => Bio.Create(textBio);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Mentor.Bio.TooLong");
            exception.Error.Message.ShouldBe("The bio must be no more than 500 characters long.");
        }

    }
}
