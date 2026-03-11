using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackForge.Application.Identity.Abstractions;
using StackForge.Application.Identity.Interfaces.Repository;
using StackForge.Application.Identity.Interfaces.Security;
using StackForge.Application.Identity.UseCases.RegisterUser;
using StackForge.Infrastructure.Data.Context;
using StackForge.Infrastructure.Data.Repositories;
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

            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<RegisterUserHandler>();
            

            return services;
        }
    }
}