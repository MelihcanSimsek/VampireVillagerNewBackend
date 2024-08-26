using Application.Services.GameSettingService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Rules
{
    public class GameStateBusinessRules
    {
        private readonly IGameStateRepository _gameStateRepository;
        private readonly IGameSettingService _gameSettingService;
        private readonly IPlayerService _playerService;

        public GameStateBusinessRules(IGameStateRepository gameStateRepository, IGameSettingService gameSettingService, IPlayerService playerService)
        {
            _gameStateRepository = gameStateRepository;
            _gameSettingService = gameSettingService;
            _playerService = playerService;
        }

    }
}
