using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Dtos
{
    public class GetListChatDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public bool IsOwner { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
