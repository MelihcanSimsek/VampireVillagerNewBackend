using Application.Services.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserOperationClaimService
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public UserOperationClaimManager(IUserOperationClaimRepository userOperationClaimRepository, IOperationClaimRepository operationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task AddStandartUserRole(User user)
        {
            OperationClaim operationClaim = await GetUserOperationClaimHasUserRole();
            UserOperationClaim claim = new() { OperationClaimId = operationClaim.Id, UserId = user.Id };
            await _userOperationClaimRepository.AddAsync(claim);
        }

        private async Task<OperationClaim> GetUserOperationClaimHasUserRole()
        {
            string roleName = "user";
            OperationClaim? operationClaim =await _operationClaimRepository.GetAsync(p => p.Name == roleName);
            if(operationClaim == null)
            {
                OperationClaim newOperationClaim = new() { Name = roleName };
                OperationClaim addedOperationClaim =await _operationClaimRepository.AddAsync(newOperationClaim);
                return addedOperationClaim;
            }
            return operationClaim;
        }
    }
}
