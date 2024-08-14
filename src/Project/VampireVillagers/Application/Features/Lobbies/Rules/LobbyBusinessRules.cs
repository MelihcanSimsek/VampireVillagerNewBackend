using Application.Features.Lobbies.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Rules
{
    public class LobbyBusinessRules
    {
        private readonly ILobbyRepository _lobbyRepository;

        public LobbyBusinessRules(ILobbyRepository lobbyRepository)
        {
            _lobbyRepository = lobbyRepository;
        }

        public async Task LobbyNameCanNotBeDuplicated(string lobbyName)
        {
            Lobby? lobby = await _lobbyRepository.GetAsync(p => p.Name == lobbyName);
            if (lobby != null) throw new BusinessException(Messages.LobbyNameAlreadyExists);
        }

        public async Task CheckLobbyPasswordNotEmptyWhenLobbyLocked(string password,bool hasPassword)
        {
            if (hasPassword && password.Trim() == "") throw new BusinessException(Messages.LobbyPasswordCanNotBeBlank);
        }

        public async Task LobbyShouldExistsWhenDeleted(Guid id)
        {
            Lobby? lobby = await _lobbyRepository.GetAsync(p => p.Id == id);
            if (lobby == null) throw new BusinessException(Messages.LobbyDoesNotExists);
        }
    }
}
