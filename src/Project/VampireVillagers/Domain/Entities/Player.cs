using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Player:Entity
    {

        public bool IsOwner { get; set; }
        public bool LiveState { get; set; }
        public int Role { get; set; }
        public int Skill { get; set; }
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }
        public virtual Lobby? Lobby { get; set; }
        public virtual User? User { get; set; }

        public Player(Guid id,bool isOwner, bool liveState, int role, int skill, Guid userId, Guid lobbyId)
        {
            Id = id;
            IsOwner = isOwner;
            LiveState = liveState;
            Role = role;
            Skill = skill;
            UserId = userId;
            LobbyId = lobbyId;
        }

        public Player()
        {
        }

       
    }
}
