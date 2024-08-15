using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Player:Entity
    {

        public bool IsOwner { get; set; }
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }
        public virtual Lobby? Lobby { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<GameState> GameStates { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Vote> VotesAsTarget { get; set; }
        public Player(bool isOwner, Guid userId, Guid lobbyId):this()
        {
            IsOwner = isOwner;
            UserId = userId;
            LobbyId = lobbyId;
        }

        public Player()
        {
        }

       
    }
}
