using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackForge.Application.Abstractions.Messaging;
using StackForge.Application.Abstractions.Persistance;
using StackForge.Application.IdentityContext.Interfaces.Repository;
using StackForge.Application.IdentityContext.Interfaces.Security;
using StackForge.Application.IdentityContext.UseCases.LoginUser;
using StackForge.Application.IdentityContext.UseCases.RegisterUser;
using StackForge.Application.MentorshipContext.UseCases.SearchMentorByStacks;
using StackForge.Application.MentorshipContext.UseCases.SearchStack;
using StackForge.Application.ProfileContext.Interfaces;
using StackForge.Application.ProfileContext.UseCases.AddStackToMentor;
using StackForge.Application.ProfileContext.UseCases.GetAllStacks;
using StackForge.Application.ProfileContext.UseCases.GetCurrentLearner;
using StackForge.Application.ProfileContext.UseCases.GetCurrentMentor;
using StackForge.Application.ProfileContext.UseCases.RegisterLearner;
using StackForge.Application.ProfileContext.UseCases.RegisterMentor;
using StackForge.Application.ProfileContext.UseCases.UpdateMentorAvailability;
using StackForge.Application.Shared.Results;
using StackForge.Application.StackContext;
using StackForge.Infrastructure.Authentication;
using StackForge.Infrastructure.Data.Context;
using StackForge.Infrastructure.Data.Repositories.Identity;
using StackForge.Infrastructure.Data.Repositories.Profile;
using StackForge.Infrastructure.Data.Repositories.StackContext;
using StackForge.Infrastructure.Data.Seed;
using StackForge.Infrastructure.Data.UnitOfWork;
using StackForge.Infrastructure.Messaging;
using StackForge.Infrastructure.Security;

namespace StackForge.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();
            services.AddSecurity();
            services.AddValidators();
            services.AddHandlers();
            services.AddMessaging();
            services.AddSeeders();

            return services;
        }

        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StackForgeDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILearnerProfileRepository, LearnerProfileRepository>();
            services.AddScoped<IMentorProfileRepository, MentorProfileRepository>();
            services.AddScoped<IStackRepository, StackRepository>();

            return services;
        }

        private static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterMentorCommand>, RegisterMentorCommandValidator>();
            services.AddScoped<IValidator<RegisterLearnerCommand>, RegisterLearnerCommandValidator>();
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<IValidator<LoginUserCommand>, LoginUserCommandValidator>();
            services.AddScoped<IValidator<AddStackToMentorCommand>, AddStackToMentorCommandValidator>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<
                ICommandHandler<AddStackToMentorCommand,  Result<AddStackToMentorResponse>>,
                AddStackToMentorHandler>();
            
            services.AddScoped<
                ICommandHandler<UpdateMentorAvailabilityCommand, Result>,
            UpdateMentorAvailabilityHandler>();

            services.AddScoped<
                IQueryHandler<GetAllStacksQuery, Result<IReadOnlyList<GetAllStacksResponse>>>,
                GetAllStacksHandler>();
            
            services.AddScoped<
                ICommandHandler<LoginUserCommand, Result<LoginUserResponse>>,
                LoginUserHandler>();

            services.AddScoped<
                ICommandHandler<RegisterUserCommand, Result<RegisterUserResponse>>,
                RegisterUserHandler>();
            
            services.AddScoped<GetCurrentMentorHandler>();
            
            services.AddScoped<
                ICommandHandler<RegisterMentorCommand, Result<RegisterMentorResponse>>,
                RegisterMentorHandler>();
            
            services.AddScoped<
                IQueryHandler<GetCurrentMentorQuery, Result<GetCurrentMentorResponse>>,
                GetCurrentMentorHandler>();
            
            services.AddScoped<
                    ICommandHandler<RegisterLearnerCommand, Result<RegisterLearnerResponse>>,
                    RegisterLearnerHandler>();
            
            services.AddScoped<
                IQueryHandler<GetCurrentLearnerQuery, Result<GetCurrentLearnerResponse>>,
                GetCurrentLearnerHandler>();
            
            services.AddScoped<
                IQueryHandler<SearchMentorByStacksQuery, Result<IReadOnlyList<SearchMentorByStacksResponse>>>,
                SearchMentorByStacksHandler>();

            services.AddScoped<
                IQueryHandler<SearchStackQuery, Result<IReadOnlyList<SearchStackResponse>>>,
                    SearchStackHandler>();


            return services;
        }

        private static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();

            return services;
        }

        private static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddScoped<StackSeeder>();

            return services;
        }
    }
}
