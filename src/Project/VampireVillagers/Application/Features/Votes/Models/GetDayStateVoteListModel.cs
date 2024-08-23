using Application.Features.Votes.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Models
{
    public class GetDayStateVoteListModel:BasePageableModel
    {
        public List<GetDayStateVoteListDto> Items { get; set; }
    }
}
