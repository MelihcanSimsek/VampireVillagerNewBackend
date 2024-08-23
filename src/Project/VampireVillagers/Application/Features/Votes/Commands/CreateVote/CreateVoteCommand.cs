using Application.Features.Votes.Dtos;
using Application.Features.Votes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands.CreateVote
{
    public class CreateVoteCommand:IRequest<CreatedVoteDto>,ISecuredRequest
    {
        public int Day { get; set; }
        public bool DayType { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TargetId { get; set; }
        public Guid GameSettingId { get; set; }
        public string[] Roles { get; } = ["user"];

        public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, CreatedVoteDto>
        {
            private readonly IVoteRepository _voteRepository;
            private readonly IMapper _mapper;
            private readonly VoteBusinessRules _voteBusinessRules;

            public CreateVoteCommandHandler(IVoteRepository voteRepository, IMapper mapper, VoteBusinessRules voteBusinessRules)
            {
                _voteRepository = voteRepository;
                _mapper = mapper;
                _voteBusinessRules = voteBusinessRules;
            }

            public async Task<CreatedVoteDto> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
            {
                await _voteBusinessRules.PlayerShouldBeExistsWhenVoteCreated(request.PlayerId);
                await _voteBusinessRules.TargetShouldBeExistsWhenVoteCreated(request.TargetId);
                await _voteBusinessRules.GameSettingShouldBeExistsWhenVoteCreated(request.GameSettingId);
                await _voteBusinessRules.VoteCanNotBeDuplicatedWhenCreated(request.GameSettingId, request.Day, request.DayType, request.PlayerId);

                Vote vote = _mapper.Map<Vote>(request);
                Vote createdVote = await _voteRepository.AddAsync(vote);
                Vote? joinedVote = (await _voteRepository.GetListAsync(p => p.Id == createdVote.Id,
                    include: m => m.Include(c => c.Player).ThenInclude(c => c.User).Include(c => c.Target).ThenInclude(c => c.User))).Items.FirstOrDefault();
                CreatedVoteDto createdVoteDto = _mapper.Map<CreatedVoteDto>(joinedVote);

                return createdVoteDto;
            }
        }

    }
}
