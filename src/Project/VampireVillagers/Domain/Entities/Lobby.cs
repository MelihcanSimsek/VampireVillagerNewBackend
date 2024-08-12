using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lobby:Entity
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<GameSetting> GameSettings { get; set; }
        public Lobby()
        {
        }

        public Lobby(Guid id, string name, DateTime creationDate)
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
        }
    }
}
