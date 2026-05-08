using StackForge.Domain.IdentityContext.Entities;
using StackForge.Domain.IdentityContext.Enums;
using StackForge.Domain.IdentityContext.ValueObjects;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.ProfileContext.Enums;
using StackForge.Domain.ProfileContext.ValueObjects;
using StackForge.Domain.StacksContext.ValueObjects;
using Stack = StackForge.Domain.StacksContext.Entities.Stack;

namespace StackForge.Application.Tests.Common;

internal static class TestData
{
    public static User CreateUser(string email = "user@example.com", string passwordHash = "hashed-password")
        => User.Create(Email.Create(email), PasswordHash.Create(passwordHash));

    public static UserRegistration CreateRegistration(
        Guid userId,
        ProfileType profileType = ProfileType.Mentor,
        bool completed = false)
    {
        var registration = UserRegistration.Create(userId, profileType);

        if (completed)
            registration.Complete();

        return registration;
    }

    public static MentorProfile CreateMentor(
        Guid? userId = null,
        string? bio = "Experienced software mentor.",
        bool withStack = false)
    {
        var mentor = MentorProfile.Create(
            Name.Create("Maria", "Silva"),
            userId ?? Guid.NewGuid(),
            new DateOnly(1990, 1, 1),
            Education.Create("Computer Science", "Stack University", EducationStatus.Completed, new DateOnly(2020, 1, 1)),
            bio is null ? null : Bio.Create(bio));

        if (withStack)
            mentor.AddStack(CreateStack());

        return mentor;
    }

    public static LearnerProfile CreateLearner(Guid? userId = null)
        => LearnerProfile.Create(
            Name.Create("Joao", "Souza"),
            userId ?? Guid.NewGuid(),
            new DateOnly(2000, 1, 1));

    public static Stack CreateStack(string name = "CSharp", string key = "csharp")
        => Stack.Create(name, Key.Create(key));
}
