using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlayerService
{
    public interface IPlayerService
    {
        Task<Player> GetPlayerById(Guid id);
        Task<List<Player>> GetAllPlayerByLobbyId(Guid lobbyId);
    }
}
