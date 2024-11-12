using Application.Features.GameStates.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Models
{
    public class DayListModel:BasePageableModel
    {
        public List<EndedDayDto> Items { get; set; }
    }
}
