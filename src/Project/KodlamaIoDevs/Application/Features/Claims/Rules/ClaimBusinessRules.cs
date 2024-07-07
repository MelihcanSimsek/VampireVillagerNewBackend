using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Rules
{
    public class ClaimBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public ClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task ClaimCanNotBeDuplicatedWhenCreated(string name)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Name == name);
            if (operationClaim != null) throw new BusinessException("OperationClaim Already Exists");
        }

        public async Task ClaimShouldBeExistWhenDeleted(int id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == id);
            if (operationClaim == null) throw new BusinessException("OperationClaim does not exists"); 
        }

        public async Task ClaimShouldBeExistWhenUpdated(int id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == id);
            if (operationClaim == null) throw new BusinessException("OperationClaim does not exists");
        }
    }
}
