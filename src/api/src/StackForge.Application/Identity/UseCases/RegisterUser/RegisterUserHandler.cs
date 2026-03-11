using StackForge.Application.Identity.Abstractions;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Identity.Interfaces.Security;
using StackForge.Application.Shared.Results;
using StackForge.Domain.Identity.Entities;
using StackForge.Domain.Identity.ValueObjects;

namespace StackForge.Application.Identity.UseCases.RegisterUser
{
    public sealed class RegisterUserHandler
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IUserRegistrationRepository _userRegistrationRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork    _unitOfWork;

        public RegisterUserHandler(IUserRepository userRepository, IUserRegistrationRepository userRegistrationRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRegistrationRepository = userRegistrationRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<RegisterUserResponse>> HandleAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            var emailExist = await _userRepository.ExistsByEmailAsync(email);

            if (emailExist)
                return Result<RegisterUserResponse>.Failure(IdentityApplicationErrors.EmailAlreadyInUse);

            var hashedPassword = _passwordHasher.Hash(command.Password);
            var passwordHash = PasswordHash.Create(hashedPassword);

            var user = User.Create(email, passwordHash);
            var userRegistration = UserRegistration.Create(user.Id, command.SelectedProfileType);
            var response = new RegisterUserResponse(user.Id);

            await _userRepository.AddAsync(user);
            await _userRegistrationRepository.AddAsync(userRegistration);
            await _unitOfWork.SaveChangesAsync();

            return Result<RegisterUserResponse>.Success(response);
        }
    }
}
