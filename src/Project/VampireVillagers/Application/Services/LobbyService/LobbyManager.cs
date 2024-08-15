using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LobbyService
{
    public class LobbyManager : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        public async Task<Lobby> GetLobbyById(Guid id)
        {
            Lobby? lobby = await _lobbyRepository.GetAsync(p => p.Id == id);
            return lobby;
        }
    }
}
