using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface ILobbyRepository : IAsyncRepository<Lobby>, IRepository<Lobby>
    {
    }

}
