using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Dtos
{
    public class CreatedPlayerDto
    {
        public Guid Id { get; set; }
        public Guid LobbyId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LobbyName { get; set; }
        public bool IsOwner { get; set; }
        
    }
}
