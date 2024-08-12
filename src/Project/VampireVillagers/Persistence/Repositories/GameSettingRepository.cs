using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class GameSettingRepository : EfRepositoryBase<GameSetting, BaseDbContext>, IGameSettingRepository
    {
        public GameSettingRepository(BaseDbContext context) : base(context)
        {
        }
    }
   
}
