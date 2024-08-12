using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IGameSettingRepository : IAsyncRepository<GameSetting>, IRepository<GameSetting>
    {
    }

}
