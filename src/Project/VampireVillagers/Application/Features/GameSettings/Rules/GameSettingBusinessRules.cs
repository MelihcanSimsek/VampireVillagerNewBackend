using Application.Features.GameSettings.Constants;
using Application.Services.LobbyService;
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
        private IGameSettingRepository _gameSettingRepository;
        private ILobbyService _lobbyService;

        public GameSettingBusinessRules(IGameSettingRepository gameSettingRepository, ILobbyService lobbyService)
        {
            _gameSettingRepository = gameSettingRepository;
            _lobbyService = lobbyService;
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
    }
}
