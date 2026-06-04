using StackForge.Application.MentorshipContext.UseCases;
using StackForge.Domain.MentorshipContext.Entities;

namespace StackForge.Application.MentorshipContext.Interfaces;

public interface IMentorshipRequestRepository
{
    Task AddAsync(MentorshipRequest mentorshipRequest);
    
    Task<IReadOnlyList<ReceivedMentorshipRequestReadModel>> GetReceivedByMentorId(Guid mentorId);
}