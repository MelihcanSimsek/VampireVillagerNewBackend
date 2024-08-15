using Application.Features.Players.Dtos;
using Application.Features.Players.Rules;
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

namespace Application.Features.Players.Commands.DeletePlayer
{
    public class DeletePlayerCommand:IRequest<DeletedPlayerDto>,ISecuredRequest
    {
        public Guid Id { get; set; }
        public string[] Roles { get; } = ["user"];

        public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, DeletedPlayerDto>
        {
            private readonly IPlayerRepository _playerRepository;
            private readonly IMapper _mapper;
            private readonly PlayerBusinessRules _playerBusinessRules;

            public DeletePlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper, PlayerBusinessRules playerBusinessRules)
            {
                _playerRepository = playerRepository;
                _mapper = mapper;
                _playerBusinessRules = playerBusinessRules;
            }

            public async Task<DeletedPlayerDto> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
            {
                await _playerBusinessRules.PlayerShoudExistsWhenDeleted(request.Id);

                Player? player = await _playerRepository.GetAsync(p => p.Id == request.Id);
                Player deletedPlayer = await _playerRepository.DeleteAsync(player);

                DeletedPlayerDto deletedPlayerDto = _mapper.Map<DeletedPlayerDto>(deletedPlayer);
                return deletedPlayerDto;
            }
        }
    }
}
