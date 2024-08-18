using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Dtos
{
    public class CreatedChatDto
    {
        public Guid Id { get; set; }
        public Guid LobbyId { get; set; }
        public Guid PlayerId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
      
    }
}
