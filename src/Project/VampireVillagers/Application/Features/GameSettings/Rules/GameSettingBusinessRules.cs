using Application.Features.GameSettings.Constants;
using Application.Services.LobbyService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Rules
{
    public class GameSettingBusinessRules
    {
        private readonly IGameSettingRepository _gameSettingRepository;
        private readonly ILobbyService _lobbyService;
        private readonly IPlayerService _playerService;

        public GameSettingBusinessRules(IGameSettingRepository gameSettingRepository, ILobbyService lobbyService, IPlayerService playerService)
        {
            _gameSettingRepository = gameSettingRepository;
            _lobbyService = lobbyService;
            _playerService = playerService;
        }

        public async Task LobbyShouldBeExistsWhenGameSettingAdded(Guid lobbyId)
        {
            Lobby? lobby = await _lobbyService.GetLobbyById(lobbyId);
            if (lobby == null) throw new BusinessException(Messages.LobbyNotFoundWhenAddingGameSetting);
        }

        public async Task GameSettingShouldBeExistsWhenDeleted(Guid id)
        {
            GameSetting? gameSetting = await _gameSettingRepository.GetAsync(p => p.Id == id);
            if (gameSetting == null) throw new BusinessException(Messages.GameSettingNotFoundWhenDeleting);
        }

        public async Task CheckPlayerNumberIsEnoughForStartingGame(GameSetting gameSetting)
        {
            int minVillagerNumber = 1;
            int minPlayerNumberForStartingGame = gameSetting.VampireNumber + gameSetting.PriestNumber + gameSetting.WitchNumber + gameSetting.VampireHunterNumber + minVillagerNumber;
            List<Player> players = await _playerService.GetAllPlayerByLobbyId(gameSetting.LobbyId);
            if (minPlayerNumberForStartingGame > players.Count) throw new BusinessException(Messages.NotEnoughPlayerForStartingGame);
        }
    }
}
