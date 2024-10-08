﻿using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options =>
                                                     options.UseNpgsql(
                                                         configuration.GetConnectionString("VampireVillagerProjectConnectionString")));

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IGameSettingRepository, GameSettingRepository>();
            services.AddScoped<ILobbyRepository, LobbyRepository>();
            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<IGameStateRepository, GameStateRepository>();

            return services;
        }
    }
}
