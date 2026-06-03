using StackForge.Application.MentorshipContext.Interfaces;
using StackForge.Domain.MentorshipContext.Entities;
using StackForge.Infrastructure.Data.Context;

namespace StackForge.Infrastructure.Data.Repositories.Mentorship;

public sealed class MentorshipRequestRepository : IMentorshipRequestRepository
{
    private readonly StackForgeDbContext _dbContext;

    public MentorshipRequestRepository(StackForgeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(MentorshipRequest mentorshipRequest)
    {
        await _dbContext.MentorshipRequests.AddAsync(mentorshipRequest);
    }
}
