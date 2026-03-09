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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork    _unitOfWork;

        public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> HandleAsync(RegisterUserCommand command)
        {
            var email = Email.Create(command.Email);

            var emailExist = await _userRepository.ExistsByEmailAsync(email);

            if (emailExist)
                return Result<Guid>.Failure(IdentityApplicationErrors.EmailAlreadyInUse);

            var hashedPassword = _passwordHasher.Hash(command.Password);
            var passwordHash = PasswordHash.Create(hashedPassword);

            var user = User.Create(email, passwordHash);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(user.Id);
        }
    }
}
