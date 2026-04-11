using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Profile.UseCases.Errors;
using StackForge.Application.Shared.Abstractions;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.UpdateMentorAvailability
{
    public sealed class UpdateMentorAvailabilityHandler
    {
        private readonly IMentorProfileRepository _mentorProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMentorAvailabilityHandler(IMentorProfileRepository mentorProfileRepository, IUnitOfWork unitOfWork)
        {
            _mentorProfileRepository = mentorProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> HandleAsync(UpdateMentorAvailabilityCommand command)
        {
            var mentor = await _mentorProfileRepository.GetByUserIdAsync(command.UserId);

            if (mentor is null)
                return Result.Failure(ProfileApplicationErrors.MentorNotFound);

            if (command.IsAvailable)
                mentor.MarkAsAvailable();
            else
                mentor.MarkAsUnavailable();

            _mentorProfileRepository.Update(mentor);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
