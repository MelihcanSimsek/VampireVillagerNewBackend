using Application.Features.Lobbies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Models
{
    public class LobbyListModel:BasePageableModel
    {
        public List<LobbyListDto> Items { get; set; }
    }
}
