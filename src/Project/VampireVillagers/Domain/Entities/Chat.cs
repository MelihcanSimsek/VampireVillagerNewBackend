using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat:Entity
    {
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public Guid LobbyId { get; set; }
        public Guid PlayerId { get; set; }
        public virtual Lobby? Lobby { get; set; }
        public virtual Player? Player { get; set; }

        public Chat(Guid id,string message, DateTime messageDate, Guid lobbyId, Guid playerId)
        {
            Id = id;
            Message = message;
            MessageDate = messageDate;
            LobbyId = lobbyId;
            PlayerId = playerId;
        }

        public Chat()
        {
        }

       
    }
}
