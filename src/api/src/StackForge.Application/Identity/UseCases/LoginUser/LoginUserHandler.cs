using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Identity.Errors;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Identity.Interfaces.Security;
using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Shared.Results;
using StackForge.Domain.IdentityContext.ValueObjects;

namespace StackForge.Application.Identity.UseCases.LoginUser
{
    public sealed class LoginUserHandler : ICommandHandler<LoginUserCommand, Result<LoginUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMentorProfileRepository _mentorProfileRepository;
        private readonly ILearnerProfileRepository _learnerProfileRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginUserHandler(
            IUserRepository userRepository, 
            IMentorProfileRepository mentorProfileRepository, 
            ILearnerProfileRepository learnerProfileRepository, 
            IPasswordHasher passwordHasher, 
            IJwtTokenGenerator jwtTokenGenerator
            )
        {
            _userRepository = userRepository;
            _mentorProfileRepository = mentorProfileRepository;
            _learnerProfileRepository = learnerProfileRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<LoginUserResponse>> HandleAsync(LoginUserCommand command)
        {
            var email = Email.Create(command.Email);
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return Result<LoginUserResponse>.Failure(LoginApplicationErrors.InvalidCredentials);

            var passswordIsValid = _passwordHasher.Verify(command.Password, user.PasswordHash);

            if (!passswordIsValid)
                return Result<LoginUserResponse>.Failure(LoginApplicationErrors.InvalidCredentials);

            var  profileTypeResult = await ResolveProfileTypeAsync(user.Id);

            if (profileTypeResult.IsFailure)
                return Result<LoginUserResponse>.Failure(profileTypeResult.Error);

            var profileType = profileTypeResult.Value;

            var accessToken = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, profileType);

            var expiresAt = _jwtTokenGenerator.GetTokenExpiration();

            var response = new LoginUserResponse(accessToken, expiresAt, profileType);

            return Result<LoginUserResponse>.Success(response);

        }

        private async Task<Result<string>> ResolveProfileTypeAsync(Guid userId)
        {
            var isMentor = await _mentorProfileRepository.ExistsByUserIdAsync(userId);
            var isLearner = await _learnerProfileRepository.ExistsByUserIdAsync(userId);

            if (isMentor && isLearner)
                return Result<string>.Failure(LoginApplicationErrors.MultipleProfileFound);

            if (isMentor)
                return Result<string>.Success("Mentor");

            if (isLearner)
                return Result<string>.Success("Learner");

            return Result<string>.Failure(LoginApplicationErrors.ProfileNotFound);
        }
    }
}
