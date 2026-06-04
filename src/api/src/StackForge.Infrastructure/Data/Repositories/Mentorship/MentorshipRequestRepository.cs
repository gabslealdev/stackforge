using Microsoft.EntityFrameworkCore;
using StackForge.Application.MentorshipContext.Interfaces;
using StackForge.Application.MentorshipContext.UseCases;
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

    public async Task<IReadOnlyList<ReceivedMentorshipRequestReadModel>> GetReceivedByMentorId(Guid mentorId)
    {
        return await (
            from request in _dbContext.MentorshipRequests.AsNoTracking()
            join learner in _dbContext.Learners.AsNoTracking()
                on request.LearnerId equals learner.Id
            join stack in _dbContext.Stacks.AsNoTracking()
                on request.StackId equals stack.Id
            where request.MentorId == mentorId
            orderby request.CreatedAt descending
            select new ReceivedMentorshipRequestReadModel(
                request.Id,
                learner.Id,
                learner.Name.ToString(),
                stack.Id,
                stack.Name,
                request.Goal.Value,
                request.Status.ToString(),
                request.CreatedAt
                )
            )
            .ToListAsync();
    }
}
