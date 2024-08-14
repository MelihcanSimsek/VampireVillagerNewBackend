using Application.Features.OperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Rules
{
    public class OperationClaimsBusinessRules
    {
        private readonly IOperationClaimRepository _operationClaimRepository;

        public OperationClaimsBusinessRules(IOperationClaimRepository operationClaimRepository)
        {
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task UserOperationClaimNameCanNotBeDuplicatedWhenCreated(string name)
        {
            OperationClaim? operationClaim =await _operationClaimRepository.GetAsync(p => p.Name == name);
            if (operationClaim != null) throw new BusinessException(Messages.OperationClaimCanNotBeDuplicated);
        }
    }
}
