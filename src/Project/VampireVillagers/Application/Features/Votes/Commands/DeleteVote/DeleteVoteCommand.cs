using Application.Features.Votes.Dtos;
using Application.Features.Votes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Commands.DeleteVote
{
    public class DeleteVoteCommand:IRequest<DeletedVoteDto>,ISecuredRequest
    {
        public Guid Id { get; set; }

        public string[] Roles { get; } = ["user"];

        public class DeleteVoteCommandHandler : IRequestHandler<DeleteVoteCommand, DeletedVoteDto>
        {
            private readonly IVoteRepository _voteRepository;
            private readonly IMapper _mapper;
            private readonly VoteBusinessRules _voteBusinessRules;

            public DeleteVoteCommandHandler(IVoteRepository voteRepository, IMapper mapper, VoteBusinessRules voteBusinessRules)
            {
                _voteRepository = voteRepository;
                _mapper = mapper;
                _voteBusinessRules = voteBusinessRules;
            }

            public async Task<DeletedVoteDto> Handle(DeleteVoteCommand request, CancellationToken cancellationToken)
            {
                await _voteBusinessRules.VoteShouldBeExistsWhenDeleted(request.Id);

                Vote? vote = await _voteRepository.GetAsync(p => p.Id == request.Id);
                Vote deletedVote = await _voteRepository.DeleteAsync(vote);
                DeletedVoteDto deletedVoteDto = _mapper.Map<DeletedVoteDto>(deletedVote);

                return deletedVoteDto;
            }
        }
    }
}
