using Bogus;
using Shouldly;
using StackForge.Domain.Identity.Enums;
using StackForge.Domain.Tests.Identity.Builders;

namespace StackForge.Domain.Tests.Identity.Entities
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
            var role = faker.PickRandom<Role>();

            // act
            Action action = () => new UserDataBuilder().WithEmail(email).WithPasswordHash(passwordHash).WithRole(role).Build();

            // assert
            action.ShouldNotThrow();
        }
    }
}
