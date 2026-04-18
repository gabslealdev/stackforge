using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Profile.UseCases.Errors;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.GetCurrentMentor
{
    public sealed class GetCurrentMentorHandler
    {
        private readonly IMentorProfileRepository _mentorProfileRepository;

        public GetCurrentMentorHandler(IMentorProfileRepository mentorProfileRepository)
        {
           _mentorProfileRepository = mentorProfileRepository;
        }

        public async Task<Result<GetCurrentMentorResponse>> HandleAsync(GetCurrentMentorQuery query)
        {
            var mentor = await _mentorProfileRepository.GetWithStacksByUserIdAsync(query.UserId);

            if (mentor is null)
                return Result<GetCurrentMentorResponse>.Failure(ProfileApplicationErrors.MentorNotFound);

            var response = new GetCurrentMentorResponse(
                mentor.UserId,
                mentor.Name.ToString(),
                mentor.Education.CourseName,
                mentor.Education.Institution,
                mentor.Bio?.ToString(),
                mentor.Availability.ToString(),
                mentor.Stacks
                    .Select(stack => new MentorStackResponse(
                        stack.Id,
                        stack.Name,
                        stack.Key.Value))
                    .ToList()
            );

            return Result<GetCurrentMentorResponse>.Success(response);
        }
    }
}
