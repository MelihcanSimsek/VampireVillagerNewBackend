using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IPlayerRepository : IAsyncRepository<Player>, IRepository<Player>
    {
    }

}
