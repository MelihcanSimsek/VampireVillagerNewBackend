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
        public virtual ICollection<Vote> Votes { get; set; }

        public GameSetting()
        {
        }

        public GameSetting(Guid id, Guid lobbyId, int nightTime, int dayTime, int vampireNumber, int priestNumber, int witchNumber, int vampireHunterNumber, int shapeshifterNumber, int transformingVampireNumber):this()
        {
            Id = id;
            LobbyId = lobbyId;
            NightTime = nightTime;
            DayTime = dayTime;
            VampireNumber = vampireNumber;
            PriestNumber = priestNumber;
            WitchNumber = witchNumber;
            VampireHunterNumber = vampireHunterNumber;
            ShapeshifterNumber = shapeshifterNumber;
            TransformingVampireNumber = transformingVampireNumber;
        }
    }
}
