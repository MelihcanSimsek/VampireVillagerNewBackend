using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.GameSettingService
{
    public class GameSettingManager : IGameSettingService
    {
        private readonly IGameSettingRepository _gameSettingRepository;

        public GameSettingManager(IGameSettingRepository gameSettingRepository)
        {
            _gameSettingRepository = gameSettingRepository;
        }

        public async Task<GameSetting?> GetGameSettingById(Guid gameSettingId)
        {
            GameSetting? gameSetting = await _gameSettingRepository.GetAsync(p => p.Id == gameSettingId);
            return gameSetting;
        }
    }
}
