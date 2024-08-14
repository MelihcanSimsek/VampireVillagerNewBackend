using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Dtos
{
    public class CreatedLobbyDto
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool HasPassword { get; set; }
        public string Password { get; set; }
    }
}
