using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Abstractions.Persistance;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Profile.Errors;
using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.Enums;
using StackForge.Domain.ProfileContext.Entities;
using StackForge.Domain.ProfileContext.Enums;
using StackForge.Domain.ProfileContext.ValueObjects;

namespace StackForge.Application.Profile.UseCases.RegisterMentor
{
    public sealed class RegisterMentorHandler 
        : ICommandHandler<RegisterMentorCommand, Result<RegisterMentorResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IMentorProfileRepository _mentorProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterMentorHandler(IUserRepository userRepository, 
            IUserRegistrationRepository userRegistrationRepository, 
            IMentorProfileRepository mentorProfileRepository, 
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRegistrationRepository = userRegistrationRepository;
            _mentorProfileRepository = mentorProfileRepository;
            _unitOfWork = unitOfWork; 
        }

        public async Task<Result<RegisterMentorResponse>> HandleAsync(RegisterMentorCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);

            if (user is null)
                return Result<RegisterMentorResponse>.Failure(ProfileApplicationErrors.UserNotFound);
            
            var userRegistration = await _userRegistrationRepository.GetByUserIdAsync(command.UserId);

            if (userRegistration is null)
                return Result<RegisterMentorResponse>.Failure(ProfileApplicationErrors.UserRegistrationNotFound);

            if (userRegistration.SelectedProfileType is not ProfileType.Mentor)
                return Result<RegisterMentorResponse>.Failure(ProfileApplicationErrors.ProfileInvalid);

            if (userRegistration.Status is RegistrationStatus.Completed)
                return Result<RegisterMentorResponse>.Failure(ProfileApplicationErrors.ProfileAlreadyExist);

            var name = Name.Create(command.FirstName, command.LastName);
            var education = Education.Create(
                command.CourseName, 
                command.Institution, 
                (EducationStatus)command.EducationStatus,
                command.ConclusionDate
                );

            Bio? bio = null;

            if (!string.IsNullOrWhiteSpace(command.Bio))
                bio = Bio.Create(command.Bio);

            var mentor = MentorProfile.Create(name, command.UserId, command.BirthDate, education, bio);

            await _mentorProfileRepository.AddAsync(mentor);

            userRegistration.Complete();
            _userRegistrationRepository.Update(userRegistration);

            await _unitOfWork.SaveChangesAsync();

            return Result<RegisterMentorResponse>.Success(new RegisterMentorResponse(mentor.Id));

        }

    }
}
