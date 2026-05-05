using Shouldly;
using StackForge.Domain.IdentityContext.ValueObjects;
using StackForge.Domain.Shared.Exceptions;

namespace StackForge.Domain.Tests.IdentityContext.ValueObjects
{
    public class EmailUnitTest
    {
        [Fact]
        public void ShouldCreate_WhenValueIsValid()
        {
            // arrange 
            var email = "test@stackforge.com.br";

            // act
            Action action = () => Email.Create(email);

            // assert
            action.ShouldNotThrow();
        }

        [Fact]
        public void ShouldThrow_WhenValueIsNullOrEmpty()
        {
            // arrange 
            var email = string.Empty;

            // act
            Action action = () => Email.Create(email);

            // assert
            var exception = action.ShouldThrow<DomainException>();

            exception.Error.Code.ShouldBe("User.Email.Required");
            exception.Error.Message.ShouldBe("Email is required.");

        }

        [Fact]
        public void ShouldThrow_WhenEmailIsTooShort()
        {
            // arrange 
            var email = "a@b.c";

            // act
            Action action = () => Email.Create(email);

            // assert
            var exception = action.ShouldThrow<DomainException>();

            exception.Error.Code.ShouldBe("User.Email.Invalid");
            exception.Error.Message.ShouldBe("Email is invalid.");
        }

        [Fact]
        public void ShouldThrow_WhenEmailIsTooLong()
        {
            // arrange 
            var email = new string('a', 245) + "@example.com";

            // act
            Action action = () => Email.Create(email);

            // assert
            var exception = action.ShouldThrow<DomainException>();

            exception.Error.Code.ShouldBe("User.Email.Invalid");
            exception.Error.Message.ShouldBe("Email is invalid.");
        }

        [Fact]
        public void ShouldThrow_WhenEmailIsInvalid()
        {
            // arrange 
            var email = "invalid-email";

            // act
            Action action = () => Email.Create(email);

            // assert
            var exception = action.ShouldThrow<DomainException>();

            exception.Error.Code.ShouldBe("User.Email.Invalid");
            exception.Error.Message.ShouldBe("Email is invalid.");
        }
    }
}
