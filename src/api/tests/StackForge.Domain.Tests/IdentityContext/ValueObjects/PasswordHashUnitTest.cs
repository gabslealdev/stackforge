using Shouldly;
using StackForge.Domain.IdentityContext.ValueObjects;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Tests.IdentityContext.ValueObjects
{
    public class PasswordHashUnitTest
    {
        [Fact]
        public void ShouldCreate_WhenValueIsValid()
        {
            // arrange 
            var passwordHash = "hashed_password";

            // act
            Action action = () => PasswordHash.Create(passwordHash);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ShouldThrow_WhenValueIsNullOrEmpty()
        {
            // arrange 
            var passwordHash = string.Empty;
            // act
            Action action = () => PasswordHash.Create(passwordHash);
            // assert
            var exception = action.ShouldThrow<DomainExceptionValidation>();

            exception.Error.Code.ShouldBe("User.Password.Required");
            exception.Error.Message.ShouldBe("Password is required.");
        }
    }

}
