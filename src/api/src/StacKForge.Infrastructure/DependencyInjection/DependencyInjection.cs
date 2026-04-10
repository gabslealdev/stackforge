using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Identity.Interfaces.Security;
using StackForge.Application.Identity.UseCases.LoginUser;
using StackForge.Application.Identity.UseCases.RegisterUser;
using StackForge.Application.Profile.Interfaces;
using StackForge.Application.Profile.UseCases.AddStackToMentor;
using StackForge.Application.Profile.UseCases.GetAllStacks;
using StackForge.Application.Profile.UseCases.RegisterLearner;
using StackForge.Application.Profile.UseCases.RegisterMentor;
using StackForge.Application.Shared.Abstractions;
using StackForge.Infrastructure.Authentication;
using StackForge.Infrastructure.Data.Context;
using StackForge.Infrastructure.Data.Repositories.Identity;
using StackForge.Infrastructure.Data.Repositories.Profile;
using StackForge.Infrastructure.Data.Seed;
using StackForge.Infrastructure.Data.UnitOfWork;
using StackForge.Infrastructure.Security;

namespace StackForge.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StackForgeDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILearnerProfileRepository, LearnerProfileRepository>();
            services.AddScoped<IMentorProfileRepository, MentorProfileRepository>();
            services.AddScoped<IValidator<RegisterMentorCommand>, RegisterMentorCommandValidator>();
            services.AddScoped<IValidator<RegisterLearnerCommand>, RegisterLearnerCommandValidator>();
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<RegisterUserHandler>();
            services.AddScoped<RegisterLearnerHandler>();
            services.AddScoped<RegisterMentorHandler>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<LoginUserHandler>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginUserCommandValidator>();
            services.AddScoped<StackSeeder>();
            services.AddScoped<IStackRepository, StackRepository>();
            services.AddScoped<GetAllStacksHandler>();
            services.AddScoped<IValidator<AddStackToMentorCommand>, AddStackToMentorCommandValidator>();
            services.AddScoped<AddStackToMentorHandler>();

            return services;
        }
    }
}