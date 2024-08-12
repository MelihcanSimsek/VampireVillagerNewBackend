using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class LobbyRepository : EfRepositoryBase<Lobby, BaseDbContext>, ILobbyRepository
    {
        public LobbyRepository(BaseDbContext context) : base(context)
        {
        }
    }
   
}
