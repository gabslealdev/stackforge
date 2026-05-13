using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;

public sealed class SearchMentorByStacksHandler 
    : IQueryHandler<SearchMentorByStacksQuery, Result<IReadOnlyList<SearchMentorByStacksResponse>>>
{
    private readonly IMentorProfileRepository _mentorProfileRepository;

    public SearchMentorByStacksHandler(IMentorProfileRepository mentorProfileRepository)
    {
        _mentorProfileRepository = mentorProfileRepository;
    }
    public async Task<Result<IReadOnlyList<SearchMentorByStacksResponse>>> HandleAsync(SearchMentorByStacksQuery query)
    {
        var mentors = await _mentorProfileRepository.SearchMentorByStacksAsync(query.StackIds);

        var response = mentors.Select(mentor => new SearchMentorByStacksResponse(
            mentor.Id,
            mentor.Name.ToString(),
            mentor.Education.CourseName,
            mentor.Education.Institution,
            mentor.Stacks.Select(stack => new MentorStackResponse(
                    stack.Id,
                    stack.Name,
                    stack.Key.Value))
                .ToList()
        )).ToList();
        
        return Result<IReadOnlyList<SearchMentorByStacksResponse>>.Success(response);
    }
}