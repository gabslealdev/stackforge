using Bogus;
using Shouldly;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.StacksContext.ValueObjects;

namespace StackForge.Domain.Tests.Stacks.ValueObjects
{
    public class KeyUnitTest
    {
        private readonly Faker _faker = new();
        [Fact]
        public void ShoudlCreate_WhenValueIsValid()
        {
            // arrange 
            var key = _faker.Random.String2(15);

            // act 
            Action action = () => Key.Create(key);

            // assert
            action.ShouldNotThrow();

        }

        [Fact]
        public void ShoudlThrow_WhenValueIsNullOrWhiteSpace()
        {
            // arrange 
            var key = string.Empty;

            // act 
            Action action = () => Key.Create(key);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Stack.Key.Required");
            exception.Error.Message.ShouldBe("Stack key is required.");
        }

        [Fact]
        public void ShoudlThrow_WhenValueIsTooLong()
        {
            // arrange
            var key = _faker.Random.String2(25);

            // act 
            Action action = () => Key.Create(key);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Stack.Key.IsTooLong");
            exception.Error.Message.ShouldBe("Stack key must be at most 20 characters long.");
        }
    }
}
