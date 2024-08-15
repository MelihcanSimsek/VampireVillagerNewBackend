using Application.Features.Players.Constants;
using Application.Services.LobbyService;
using Application.Services.Repositories;
using Application.Services.UserService;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Rules
{
    public class PlayerBusinessRules
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserService _userService;
        private readonly ILobbyService _lobbyService;

        public PlayerBusinessRules(IPlayerRepository playerRepository, IUserService userService, ILobbyService lobbyService)
        {
            _playerRepository = playerRepository;
            _userService = userService;
            _lobbyService = lobbyService;
        }

        public async Task PlayerShoudExistsWhenDeleted(Guid id)
        {
            Player? player = await _playerRepository.GetAsync(p => p.Id == id);
            if (player == null) throw new BusinessException(Messages.PlayerNotFound);
        }

        public async Task UserShouldBeExistsWhenPlayerCreated(Guid id)
        {
            User? user = await _userService.GetUserById(id);
            if (user == null) throw new BusinessException(Messages.UserNotFoundWhenAddingPlayer);
        }

        public async Task LobbyShouldBeExistsWhenPlayerCreated(Guid id)
        {
            Lobby? lobby = await _lobbyService.GetLobbyById(id);
            if (lobby == null) throw new BusinessException(Messages.LobbyNotFoundWhenAddingPlayer);
        }
    }
}
