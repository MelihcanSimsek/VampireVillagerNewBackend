using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VoteRepository : EfRepositoryBase<Vote, BaseDbContext>, IVoteRepository
    {
        public VoteRepository(BaseDbContext context) : base(context)
        {
        }
    }
   
}
