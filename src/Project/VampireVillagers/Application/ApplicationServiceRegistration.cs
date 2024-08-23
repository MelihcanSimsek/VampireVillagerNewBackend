using Application.Features.Auths.Rules;
using Application.Features.Chats.Rules;
using Application.Features.GameSettings.Rules;
using Application.Features.Lobbies.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Players.Rules;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Votes.Rules;
using Application.Services.AuthService;
using Application.Services.GameSettingService;
using Application.Services.LobbyService;
using Application.Services.OperationClaimService;
using Application.Services.PlayerService;
using Application.Services.UserOperationClaimService;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<LobbyBusinessRules>();
            services.AddScoped<OperationClaimsBusinessRules>();
            services.AddScoped<UserOperationClaimsBusinessRules>();
            services.AddScoped<PlayerBusinessRules>();
            services.AddScoped<ChatBusinessRules>();
            services.AddScoped<GameSettingBusinessRules>();
            services.AddScoped<VoteBusinessRules>();
            

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IOperationClaimService, OperationClaimManager>();
            services.AddScoped<ILobbyService, LobbyManager>();
            services.AddScoped<IPlayerService, PlayerManager>();
            services.AddScoped<IGameSettingService, GameSettingManager>();


            return services;

        }
    }
}
