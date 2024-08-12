using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities
{
    public class Vote:Entity
    {
        public int Day { get; set; }
        public bool DayType { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TargetId { get; set; }
        public Guid GameSettingId { get; set; }
        public virtual Player? Player { get; set; }
        public virtual Player? Target { get; set; }
        public virtual GameSetting? GameSetting { get; set; }

        public Vote(Guid id,int day, bool dayType, Guid playerId, Guid targetId, Guid gameSettingId)
        {
            Id = id;
            Day = day;
            DayType = dayType;
            PlayerId = playerId;
            TargetId = targetId;
            GameSettingId = gameSettingId;
        }

        public Vote()
        {
        }
    }
}
