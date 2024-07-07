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

namespace Application.Features.Claims.Commands.DeleteClaim
{
    public class DeleteClaimCommand:IRequest<DeletedOperationClaimDto>
    {
        public int Id { get; set; }

        public class DeleteClaimCommandHandler : IRequestHandler<DeleteClaimCommand, DeletedOperationClaimDto>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMapper _mapper;
            private readonly ClaimBusinessRules _claimBusinessRules;

            public DeleteClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, ClaimBusinessRules claimBusinessRules)
            {
                _operationClaimRepository = operationClaimRepository;
                _mapper = mapper;
                _claimBusinessRules = claimBusinessRules;
            }

            public async Task<DeletedOperationClaimDto> Handle(DeleteClaimCommand request, CancellationToken cancellationToken)
            {
                await _claimBusinessRules.ClaimShouldBeExistWhenDeleted(request.Id);

                OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(p => p.Id == request.Id);
                OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);
                DeletedOperationClaimDto deletedOperationClaimDto = _mapper.Map<DeletedOperationClaimDto>(deletedOperationClaim);

                return deletedOperationClaimDto;
            }
        }
    }
}
