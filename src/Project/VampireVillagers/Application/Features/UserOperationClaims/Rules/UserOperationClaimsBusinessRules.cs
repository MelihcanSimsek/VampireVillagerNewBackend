using Application.Features.UserOperationClaims.Constants;
using Application.Services.OperationClaimService;
using Application.Services.Repositories;
using Application.Services.UserService;
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
        private readonly IUserService _userService;
        private readonly IOperationClaimService _operationClaimService;

        public UserOperationClaimsBusinessRules(IUserOperationClaimRepository userOperationClaimRepository, IUserService userService, IOperationClaimService operationClaimService)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userService = userService;
            _operationClaimService = operationClaimService;
        }

        public async Task UserOperationClaimShouldExistsWhenDeleted(Guid id)
        {
            UserOperationClaim? claim = await _userOperationClaimRepository.GetAsync(p => p.Id == id);
            if (claim == null) throw new BusinessException(Messages.UserOperationClaimNotFound);
        }


        public async Task UserShouldBeExistsWhenUserOperationClaimCreated(Guid id)
        {
            User? user = await _userService.GetUserById(id);
            if (user == null) throw new BusinessException(Messages.UserNotFoundWhenUserOperationClaimAdding);
        }

        public async Task OperationClaimShouldBeExistsWhenUserOperationClaimCreated(Guid id)
        {
            OperationClaim? operationClaim = await _operationClaimService.GetOperationClaimById(id);
            if (operationClaim == null) throw new BusinessException(Messages.OperationClaimNotFoundWhenUserOperationClaimAdding);
        }
    }
}
