using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameSetting:Entity
    {
        public DateTime CreationDate { get; set; }    
        public int NightTime { get; set; }
        public int DayTime { get; set; }
        public int VampireNumber { get; set; }
        public int PriestNumber { get; set; }
        public int WitchNumber { get; set; }
        public int VampireHunterNumber { get; set; }
        public int ShapeshifterNumber { get; set; }
        public int TransformingVampireNumber { get; set; }
        public Guid LobbyId { get; set; }
        public virtual Lobby? Lobby { get; set; }
        public virtual ICollection<GameState> GameStates { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public GameSetting()
        {
        }

        public GameSetting(DateTime creationDate, int nightTime, int dayTime, int vampireNumber, int priestNumber, int witchNumber, int vampireHunterNumber, int shapeshifterNumber, int transformingVampireNumber, Guid lobbyId):this()
        {
            CreationDate = creationDate;
            NightTime = nightTime;
            DayTime = dayTime;
            VampireNumber = vampireNumber;
            PriestNumber = priestNumber;
            WitchNumber = witchNumber;
            VampireHunterNumber = vampireHunterNumber;
            ShapeshifterNumber = shapeshifterNumber;
            TransformingVampireNumber = transformingVampireNumber;
            LobbyId = lobbyId;
        }
    }
}
