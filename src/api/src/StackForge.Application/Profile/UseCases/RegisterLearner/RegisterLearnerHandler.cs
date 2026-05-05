using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Profile.Errors;
using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Shared.Abstractions;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.Enums;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.ProfileContext.ValueObjects;

namespace StackForge.Application.Profile.UseCases.RegisterLearner
{
    public sealed class RegisterLearnerHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly ILearnerProfileRepository _learnerProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterLearnerHandler(IUserRepository userRepository, IUserRegistrationRepository userRegistrationRepository, ILearnerProfileRepository learnerProfileRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRegistrationRepository = userRegistrationRepository;
            _learnerProfileRepository = learnerProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterLearnerResponse>> HandleAsync(RegisterLearnerCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user is null)
                return Result<RegisterLearnerResponse>.Failure(ProfileApplicationErrors.UserNotFound);

            var userRegistration = await _userRegistrationRepository.GetByUserIdAsync(command.UserId);

            if (userRegistration is null)
                return Result<RegisterLearnerResponse>.Failure(ProfileApplicationErrors.UserNotFound);


            if (userRegistration.SelectedProfileType is not ProfileType.Learner)
                return Result<RegisterLearnerResponse>.Failure(ProfileApplicationErrors.ProfileInvalid);

            if (userRegistration.Status is RegistrationStatus.Completed)
                return Result<RegisterLearnerResponse>.Failure(ProfileApplicationErrors.ProfileAlreadyExist);

            var learner = LearnerProfile.Create(Name.Create(command.FirstName, command.LastName), command.UserId, command.BirthDate);

            await _learnerProfileRepository.AddAsync(learner);

            userRegistration.Complete();
            _userRegistrationRepository.Update(userRegistration);

            await _unitOfWork.SaveChangesAsync();

            return Result<RegisterLearnerResponse>.Success(new RegisterLearnerResponse(learner.Id));
        }
    }
}
