using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Dtos
{
    public class GetDayStateVoteListDto
    {
        public Guid Id { get; set; }
        public int Day { get; set; }
        public bool DayType { get; set; }
        public string PlayerName { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TargetId { get; set; }
        public Guid GameSettingId { get; set; }
    }
}
