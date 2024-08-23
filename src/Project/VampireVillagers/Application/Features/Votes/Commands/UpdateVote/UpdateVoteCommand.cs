using Application.Features.Votes.Dtos;
using Application.Features.Votes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands.UpdateVote
{
    public class UpdateVoteCommand : IRequest<UpdatedVoteDto>,ISecuredRequest
    {
        public int Day { get; set; }
        public bool DayType { get; set; }
        public Guid PlayerId { get; set; }
        public Guid TargetId { get; set; }
        public Guid GameSettingId { get; set; }
        public string[] Roles { get; } = ["user"];

        public class UpdateVoteCommandHandler : IRequestHandler<UpdateVoteCommand, UpdatedVoteDto>
        {
            private readonly IVoteRepository _voteRepository;
            private readonly IMapper _mapper;
            private readonly VoteBusinessRules _voteBusinessRules;

            public UpdateVoteCommandHandler(IVoteRepository voteRepository, IMapper mapper, VoteBusinessRules voteBusinessRules)
            {
                _voteRepository = voteRepository;
                _mapper = mapper;
                _voteBusinessRules = voteBusinessRules;
            }

            public async Task<UpdatedVoteDto> Handle(UpdateVoteCommand request, CancellationToken cancellationToken)
            {
                await _voteBusinessRules.VoteShouldBeExistsWhenUpdated(request.GameSettingId, request.Day, request.DayType, request.PlayerId);

                Vote? vote = await _voteRepository.GetAsync(p => p.PlayerId == request.PlayerId
                                                            && p.DayType == request.DayType
                                                            && p.GameSettingId == request.GameSettingId
                                                            && p.Day == request.Day);
                vote.TargetId = request.TargetId;
                Vote updatedVote = await _voteRepository.UpdateAsync(vote);
                Vote? joinedVote = (await _voteRepository.GetListAsync(p => p.Id == updatedVote.Id,
                    include: m => m.Include(c => c.Player).ThenInclude(c => c.User).Include(c=>c.Target).ThenInclude(c=>c.User))).Items.FirstOrDefault();
                UpdatedVoteDto updatedVoteDto = _mapper.Map<UpdatedVoteDto>(joinedVote);

                return updatedVoteDto;
            }
        }
    }
}
