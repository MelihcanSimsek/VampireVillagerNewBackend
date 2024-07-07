using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public class CreateUserOperationClaimCommand:IRequest<CreatedUserOperationClaimDto>,ISecuredRequest
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public string[] Roles { get; } = ["admin", "mod"];
        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public CreateUserOperationClaimCommandHandler(IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _mapper = mapper;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<CreatedUserOperationClaimDto> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.UserOperationClaimCanNotBeDuplicatedWhenCreated(request.UserId, request.OperationClaimId);

                UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(request);
                UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(userOperationClaim);
                CreatedUserOperationClaimDto createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimDto>(createdUserOperationClaim);

                return createdUserOperationClaimDto;
            }
        }
    }
}
