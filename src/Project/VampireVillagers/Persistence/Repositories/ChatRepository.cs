using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ChatRepository : EfRepositoryBase<Chat, BaseDbContext>, IChatRepository
    {
        public ChatRepository(BaseDbContext context) : base(context)
        {
        }
    }
   
}
