using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task UserOperationClaimCanNotBeDuplicatedWhenCreated(int userId,int operationClaimId)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(p => p.UserId == userId && p.OperationClaimId == operationClaimId);
            if (userOperationClaim != null) throw new BusinessException("UserOperationClaim already exists");
        }

        public async Task UserOperationClaimShouldBeExistsWhenDeleted(int id)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(p => p.Id == id);
            if (userOperationClaim == null) throw new BusinessException("UserOperationClaim does not exists");
        }

        public async Task CheckUserAlreadyExistWhenRequested(int userId)
        {
            UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(p => p.UserId == userId);
            if (result == null) throw new BusinessException("UserOperationClaim does not exists when requested");
        }
    }
}
