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

        public async Task<Player> GetPlayerById(Guid id)
        {
            Player? player = await _playerRepository.GetAsync(p => p.Id == id);
            return player;
        }
    }
}
