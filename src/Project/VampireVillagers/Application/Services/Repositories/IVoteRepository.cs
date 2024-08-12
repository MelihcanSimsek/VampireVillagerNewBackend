using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IVoteRepository : IAsyncRepository<Vote>, IRepository<Vote>
    {
    }

}
