using Application.Features.Claims.Dtos;
using Application.Features.Claims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Claims.Commands.UpdateClaim
{
    public class UpdateClaimCommand : IRequest<UpdatedOperationClaimDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, UpdatedOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public UpdateClaimCommandHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository, ClaimBusinessRules claimBusinessRules)
            {
                _mapper = mapper;
                _operationClaimRepository = operationClaimRepository;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<UpdatedOperationClaimDto> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
            {
                await _claimBusinessRules.ClaimShouldBeExistWhenUpdated(request.Id);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == request.Id);
                operationClaim.Name = request.Name;

                OperationClaim updatedOperationClaim = await _operationClaimRepository.AddAsync(operationClaim);
                UpdatedOperationClaimDto updatedOperationClaimDto = _mapper.Map<UpdatedOperationClaimDto>(updatedOperationClaim);

                return updatedOperationClaimDto;
            }
        }
    }
}
