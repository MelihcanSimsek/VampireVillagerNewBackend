using Application.Features.UserOperationClaims.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Rules
{
    public class UserOperationClaimsBusinessRules
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;

        public UserOperationClaimsBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository, IOperationClaimRepository operationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userRepository = userRepository;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task UserOperationClaimShouldExistsWhenDeleted(Guid id)
        {
            UserOperationClaim? claim = await _userOperationClaimRepository.GetAsync(p => p.Id == id);
            if (claim == null) throw new BusinessException(Messages.UserOperationClaimNotFound);
        }


        public async Task UserShouldBeExistsWhenUserOperationClaimCreated(Guid id)
        {
            User? user = await _userRepository.GetAsync(p => p.Id == id);
            if (user == null) throw new BusinessException(Messages.UserNotFoundWhenUserOperationClaimAdding);
        }

        public async Task OperationClaimShouldBeExistsWhenUserOperationClaimCreated(Guid id)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == id);
            if (operationClaim == null) throw new BusinessException(Messages.OperationClaimNotFoundWhenUserOperationClaimAdding);
        }
    }
}
