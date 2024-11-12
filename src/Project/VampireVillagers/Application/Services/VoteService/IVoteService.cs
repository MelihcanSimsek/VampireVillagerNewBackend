using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.VoteService
{
    public interface IVoteService
    {
        Task<Player> GetHangedPlayer(Guid gameSettingId,int day,bool dayType);
    }
}
