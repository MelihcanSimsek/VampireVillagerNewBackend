using Application.Services.LobbyService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Chats.Constants;

namespace Application.Features.Chats.Rules
{
    public class ChatBusinessRules
    {
        private readonly IChatRepository _chatRepository;
        private readonly IPlayerService _playerService;
        private readonly ILobbyService _lobbyService;

        public ChatBusinessRules(IChatRepository chatRepository, IPlayerService playerService, ILobbyService lobbyService)
        {
            _chatRepository = chatRepository;
            _playerService = playerService;
            _lobbyService = lobbyService;
        }

        public async Task ChatShouldBeExistsWhenDeleted(Guid chatId)
        {
            Chat? chat = await _chatRepository.GetAsync(p => p.Id == chatId);
            if (chat == null) throw new BusinessException(Messages.ChatNotFoundWhenDeleted);
        }

        public async Task LobbyShouldBeExistsWhenChatCreated(Guid lobbyId)
        {
            Lobby? lobby = await _lobbyService.GetLobbyById(lobbyId);
            if (lobby == null) throw new BusinessException(Messages.LobbyNotFoundWhenAddingChat);
        }

        public async Task PlayerShouldBeExistsWhenChatCreated(Guid playerId)
        {
            Player? player = await _playerService.GetPlayerById(playerId);
            if (player == null) throw new BusinessException(Messages.PlayerNotFoundWhenAddingChat);
        }
    }
}
