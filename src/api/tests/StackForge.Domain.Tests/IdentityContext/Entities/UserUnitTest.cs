using Bogus;
using Shouldly;
using StackForge.Domain.Tests.IdentityContext.Builders;

namespace StackForge.Domain.Tests.IdentityContext.Entities
{
    public class UserUnitTest
    {
        [Fact]
        public void ShouldCreate_WhenIsValid()
        {
            // arrange 
            var faker = new Faker();
            var email = faker.Internet.Email();
            var passwordHash = faker.Random.Hash();

            // act
            Action action = () => new UserBuilder().WithEmail(email).WithPasswordHash(passwordHash);

            // assert
            action.ShouldNotThrow();
        }
    }
}
