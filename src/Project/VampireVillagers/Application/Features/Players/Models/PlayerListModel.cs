
using Application.Features.Players.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Models
{
    public class PlayerListModel:BasePageableModel
    {
        public List<PlayerGetListDto> Items { get; set; }
    }
}
