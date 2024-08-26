using Application.Services.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlayerService
{
    public class PlayerManager : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerManager(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<List<Player>> GetAllPlayerByLobbyId(Guid lobbyId)
        {
            List<Player> players = (await _playerRepository.GetListAsync(p => p.LobbyId == lobbyId)).Items.ToList();
            return players;
        }

        public async Task<Player> GetPlayerById(Guid id)
        {
            Player? player = await _playerRepository.GetAsync(p => p.Id == id);
            return player;
        }
    }
}
