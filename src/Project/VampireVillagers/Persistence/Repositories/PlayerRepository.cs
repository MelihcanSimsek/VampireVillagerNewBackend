using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class PlayerRepository : EfRepositoryBase<Player, BaseDbContext>, IPlayerRepository
    {
        public PlayerRepository(BaseDbContext context) : base(context)
        {
        }
    }
   
}
