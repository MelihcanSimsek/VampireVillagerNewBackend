using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Dtos
{
    public class GetGameStateDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid LobbyId { get; set; }
        public Guid GameSettingId { get; set; }
        public string PlayerName { get; set; }
        public bool IsOwner { get; set; }
        public bool LiveState { get; set; }
        public int Role { get; set; }
        public int Skill { get; set; }
    }
}
