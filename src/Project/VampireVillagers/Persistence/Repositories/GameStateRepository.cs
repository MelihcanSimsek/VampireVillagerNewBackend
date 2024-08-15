using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class GameStateRepository : EfRepositoryBase<GameState, BaseDbContext>, IGameStateRepository
    {
        public GameStateRepository(BaseDbContext context) : base(context)
        {
        }
    }

}
