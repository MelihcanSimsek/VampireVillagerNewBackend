using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Dtos
{
    public class CreatedGameSettingDto
    {
        public Guid Id { get; set; }
        public Guid LobbyId { get; set; }
        public DateTime CreationDate { get; set; }
        public int NightTime { get; set; }
        public int DayTime { get; set; }
        public int VampireNumber { get; set; }
        public int PriestNumber { get; set; }
        public int WitchNumber { get; set; }
        public int VampireHunterNumber { get; set; }
        public int ShapeshifterNumber { get; set; }
        public int TransformingVampireNumber { get; set; }
    }
}
