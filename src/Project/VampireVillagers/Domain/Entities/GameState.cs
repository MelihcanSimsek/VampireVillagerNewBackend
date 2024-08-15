using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameState:Entity
    {
        public bool LiveState { get; set; }
        public int Role { get; set; }
        public int Skill { get; set; }
        public Guid PlayerId { get; set; }
        public Guid GameSettingId { get; set; }
        public virtual Player? Player { get; set; }
        public virtual GameSetting? GameSetting { get; set; }

        public GameState()
        {
        }

        public GameState(Guid id,bool liveState, int role, int skill, Guid playerId, Guid gameSettingId):this()
        {
            Id = id;
            LiveState = liveState;
            Role = role;
            Skill = skill;
            PlayerId = playerId;
            GameSettingId = gameSettingId;
        }
    }
}
