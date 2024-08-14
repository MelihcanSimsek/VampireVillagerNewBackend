using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserOperationClaimService
{
    public interface IUserOperationClaimService
    {
        public Task AddStandartUserRole(User user);
    }
}
