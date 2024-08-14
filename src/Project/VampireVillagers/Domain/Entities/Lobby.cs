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
        public bool HasPassword { get; set; }
        public string? Password { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<GameSetting> GameSettings { get; set; }
        public Lobby()
        {
        }

        public Lobby(Guid id, string name, DateTime creationDate,bool hasPassword,string password):this()
        {
            Id = id;
            Name = name;
            CreationDate = creationDate;
            HasPassword = hasPassword;
            Password = password;
        }
    }
}
