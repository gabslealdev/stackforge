using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Profile.UseCases.Errors;
using StackForge.Application.Shared.Abstractions;
using StackForge.Application.Shared.Results;

namespace StackForge.Application.Profile.UseCases.AddStackToMentor
{
    public sealed class AddStackToMentorHandler
    {
        private readonly IMentorProfileRepository _mentorProfileRepository;
        private readonly IStackRepository _stackRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddStackToMentorHandler(IMentorProfileRepository mentorProfileRepository, IStackRepository stackRepository, IUnitOfWork unitOfWork)
        {
            _mentorProfileRepository = mentorProfileRepository;
            _stackRepository = stackRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AddStackToMentorResponse>> HandleAsync(AddStackToMentorCommand command)
        {
            var mentor = await _mentorProfileRepository.GetByUserIdAsync(command.UserId);

            if (mentor is null)
                return Result<AddStackToMentorResponse>.Failure(ProfileApplicationErrors.MentorNotFound);

            var stack = await _stackRepository.GetByIdAsync(command.StackId);

            if (stack is null)
                return Result<AddStackToMentorResponse>.Failure(ProfileApplicationErrors.StackNotFound);

            mentor.AddStack(stack);


            _mentorProfileRepository.Update(mentor);

            await _unitOfWork.SaveChangesAsync();

            return Result<AddStackToMentorResponse>.Success(new AddStackToMentorResponse(stack.Id, stack.Name));
        }
    }
}
