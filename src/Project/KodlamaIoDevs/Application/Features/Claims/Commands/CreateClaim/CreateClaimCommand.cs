using Application.Features.Claims.Dtos;
using Application.Features.Claims.Rules;
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

namespace Application.Features.Claims.Commands.CreateClaim
{
    public class CreateClaimCommand:IRequest<CreatedOperationClaimDto>,ISecuredRequest
    {
        public string Name { get; set; }
        public string[] Roles { get; } = ["admin", "mod"];
        public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, CreatedOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public CreateClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, ClaimBusinessRules claimBusinessRules)
            {
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<CreatedOperationClaimDto> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
            {
                await _claimBusinessRules.ClaimCanNotBeDuplicatedWhenCreated(request.Name);

                OperationClaim addedClaim = _mapper.Map<OperationClaim>(request);
                OperationClaim createdClaim =await _operationClaimRepository.AddAsync(addedClaim);
                CreatedOperationClaimDto createdOperationClaimDto = _mapper.Map<CreatedOperationClaimDto>(createdClaim);

                return createdOperationClaimDto;
            }
        }
    }
}
