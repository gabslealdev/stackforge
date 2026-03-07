using Bogus;
using Shouldly;
using StackForge.Domain.Shared.Exceptions;
using StackForge.Domain.Stacks.Entities;
using StackForge.Domain.Tests.Stacks.Builders;

namespace StackForge.Domain.Tests.Stacks.Entities
{
    public class StackUnitTest
    {
        private readonly Faker _faker = new();

        [Fact]
        public void ShouldCreate_WhenStackIsValid()
        {
            // arrange
            var name = _faker.Random.Word();
            var key = _faker.Random.String2(15);

            // act 
            Action action = () => Stack.Create(name, key);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ShouldThrow_WhenNameIsNullOrWhiteSpace()
        {
            // arrange
            var name = string.Empty;
            var key = _faker.Random.String2(15);

            // act 
            Action action = () => Stack.Create(name, key);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Stack.Name.Required");
            exception.Error.Message.ShouldBe("Stack name is required.");
        }

        [Fact]
        public void ShouldThrow_WhenNameIsTooLong()
        {
            // arrange
            var name = _faker.Random.String2(25);
            var key = _faker.Random.String2(15);

            // act 
            Action action = () => Stack.Create(name, key);

            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();
            exception.Error.Code.ShouldBe("Stack.Name.IsTooLong");
            exception.Error.Message.ShouldBe("Stack name must be at most 20 characters long.");
        }

    }
}
