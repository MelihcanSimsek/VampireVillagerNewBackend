using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IGameStateRepository : IAsyncRepository<GameState>, IRepository<GameState>
    {
    }
}
