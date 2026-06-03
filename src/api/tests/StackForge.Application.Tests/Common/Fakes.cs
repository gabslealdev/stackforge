using StackForge.Application.Abstractions.Persistance;
using StackForge.Application.IdentityContext.Interfaces.Repository;
using StackForge.Application.IdentityContext.Interfaces.Security;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.StackContext;
using StackForge.Domain.IdentityContext.Entities;
using StackForge.Domain.IdentityContext.ValueObjects;
using StackForge.Domain.ProfileContext.Entities;
using Stack = StackForge.Domain.StacksContext.Entities.Stack;

namespace StackForge.Application.Tests.Common;

internal sealed class FakeUnitOfWork : IUnitOfWork
{
    public int SaveChangesCount { get; private set; }

    public Task SaveChangesAsync()
    {
        SaveChangesCount++;
        return Task.CompletedTask;
    }
}

internal sealed class FakeUserRepository : IUserRepository
{
    public bool ExistsByEmailResult { get; set; }
    public User? UserById { get; set; }
    public User? UserByEmail { get; set; }
    public List<User> AddedUsers { get; } = [];

    public Task<bool> ExistsByEmailAsync(Email email)
        => Task.FromResult(ExistsByEmailResult);

    public Task AddAsync(User user)
    {
        AddedUsers.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid id)
        => Task.FromResult(UserById is not null && UserById.Id == id ? UserById : null);

    public Task<User?> GetByEmailAsync(Email email)
        => Task.FromResult(UserByEmail is not null && UserByEmail.Email == email ? UserByEmail : null);
}

internal sealed class FakeUserRegistrationRepository : IUserRegistrationRepository
{
    public UserRegistration? RegistrationByUserId { get; set; }
    public List<UserRegistration> AddedRegistrations { get; } = [];
    public List<UserRegistration> UpdatedRegistrations { get; } = [];

    public Task AddAsync(UserRegistration userRegistration)
    {
        AddedRegistrations.Add(userRegistration);
        return Task.CompletedTask;
    }

    public Task<UserRegistration?> GetByUserIdAsync(Guid userId)
        => Task.FromResult(RegistrationByUserId is not null && RegistrationByUserId.UserId == userId ? RegistrationByUserId : null);

    public void Update(UserRegistration userRegistration)
        => UpdatedRegistrations.Add(userRegistration);
}

internal sealed class FakePasswordHasher : IPasswordHasher
{
    public string HashResult { get; set; } = "hashed-password";
    public bool VerifyResult { get; set; } = true;
    public List<string> HashedPasswords { get; } = [];
    public List<(string Password, PasswordHash PasswordHash)> VerifiedPasswords { get; } = [];

    public string Hash(string password)
    {
        HashedPasswords.Add(password);
        return HashResult;
    }

    public bool Verify(string password, PasswordHash passwordHash)
    {
        VerifiedPasswords.Add((password, passwordHash));
        return VerifyResult;
    }
}

internal sealed class FakeJwtTokenGenerator : IJwtTokenGenerator
{
    public string AccessToken { get; set; } = "access-token";
    public DateTimeOffset Expiration { get; set; } = new(2030, 1, 1, 0, 0, 0, TimeSpan.Zero);
    public List<(Guid UserId, Email Email, string ProfileType)> GeneratedTokens { get; } = [];

    public string GenerateToken(Guid userId, Email email, string profileType)
    {
        GeneratedTokens.Add((userId, email, profileType));
        return AccessToken;
    }

    public DateTimeOffset GetTokenExpiration()
        => Expiration;
}

internal sealed class FakeLearnerProfileRepository : ILearnerProfileRepository
{
    public bool ExistsByUserIdResult { get; set; }
    public List<LearnerProfile> AddedLearners { get; } = [];
    public LearnerProfile? LearnerByUserId { get; set; }

    public Task AddAsync(LearnerProfile learnerProfile)
    {
        AddedLearners.Add(learnerProfile);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByUserIdAsync(Guid userId)
        => Task.FromResult(ExistsByUserIdResult);

    public Task<LearnerProfile?> GetByUserIdAsync(Guid userId)
        => Task.FromResult(LearnerByUserId is not null && LearnerByUserId.Id == userId ? LearnerByUserId : null);
    
}

internal sealed class FakeMentorProfileRepository : IMentorProfileRepository
{
    public bool ExistsByUserIdResult { get; set; }
    public MentorProfile? MentorByUserId { get; set; }
    public MentorProfile? MentorWithStacksByUserId { get; set; }
    public List<MentorProfile> AddedMentors { get; } = [];
    public List<MentorProfile> UpdatedMentors { get; } = [];

    public Task AddAsync(MentorProfile mentorProfile)
    {
        AddedMentors.Add(mentorProfile);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsByUserIdAsync(Guid userId)
        => Task.FromResult(ExistsByUserIdResult);

    public Task<MentorProfile?> GetByUserIdAsync(Guid userId)
        => Task.FromResult(MentorByUserId is not null && MentorByUserId.UserId == userId ? MentorByUserId : null);

    public Task<MentorProfile?> GetByMentorIdAsync(Guid mentorId)
    {
        throw new NotImplementedException();
    }

    public Task<MentorProfile?> GetWithStacksByUserIdAsync(Guid userId)
        => Task.FromResult(MentorWithStacksByUserId is not null && MentorWithStacksByUserId.UserId == userId ? MentorWithStacksByUserId : null);

    public void Update(MentorProfile mentorProfile)
        => UpdatedMentors.Add(mentorProfile);

    public Task<IReadOnlyList<MentorProfile>> SearchMentorByStacksAsync(IReadOnlyList<Guid> stackIds)
    {
        throw new NotImplementedException();
    }
}

internal sealed class FakeStackRepository : IStackRepository
{
    public IReadOnlyList<Stack> Stacks { get; set; } = [];
    public Stack? StackById { get; set; }

    public Task<IReadOnlyList<Stack>> GetAllOrderedByNameAsync()
        => Task.FromResult(Stacks);

    public Task<Stack?> GetByIdAsync(Guid stackId)
        => Task.FromResult(StackById is not null && StackById.Id == stackId ? StackById : null);

    public Task<IReadOnlyList<Stack>> SearchByTermAsync(string term)
    {
        throw new NotImplementedException();
    }
}
