using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Queries.GetListByIdUserOperationClaim
{
    public class GetListByIdUserOperationClaimQuery:IRequest<GetListByIdUserOperationClaimDto>
    {
        public int UserId { get; set; }

        public class GetListByIdUserOperationClaimQueryHandler : IRequestHandler<GetListByIdUserOperationClaimQuery, GetListByIdUserOperationClaimDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

            public GetListByIdUserOperationClaimQueryHandler(IMapper mapper, IUserOperationClaimRepository userOperationClaimRepository, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
            {
                _mapper = mapper;
                _userOperationClaimRepository = userOperationClaimRepository;
                _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            }

            public async Task<GetListByIdUserOperationClaimDto> Handle(GetListByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                await _userOperationClaimBusinessRules.CheckUserAlreadyExistWhenRequested(request.UserId);

                IPaginate<UserOperationClaim>? userOperationClaims = await _userOperationClaimRepository.GetListAsync(c=>c.UserId == request.UserId,include:p=>p.Include(u=>u.OperationClaim).Include(u=>u.User));

                IList<string> operationClaims = userOperationClaims.Items.Select(p => p.OperationClaim.Name).ToList();
                GetListByIdUserOperationClaimDto result = new()
                {
                    UserId = userOperationClaims.Items.FirstOrDefault().UserId,
                    FirstName = userOperationClaims.Items.FirstOrDefault().User.FirstName,
                    LastName = userOperationClaims.Items.FirstOrDefault().User.LastName,
                    OperationNames = operationClaims.ToArray()
                };

                return result;
            }
        }
    }
}
