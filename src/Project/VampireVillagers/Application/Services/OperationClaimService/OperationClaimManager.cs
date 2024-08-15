using Application.Services.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OperationClaimService
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimManager(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<OperationClaim> GetOperationClaimById(Guid id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == id);
            return operationClaim;
        }
    }
}
