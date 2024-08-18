using Application.Features.Chats.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Models
{
    public class ChatListModel:BasePageableModel
    {
        public List<GetListChatDto> Items { get; set; }
    }
}
