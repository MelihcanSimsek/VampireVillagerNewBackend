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

namespace Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand:IRequest<CreatedPlayerDto>,ISecuredRequest
    {
        public bool IsOwner { get; set; }
        public Guid UserId { get; set; }
        public Guid LobbyId { get; set; }
        public string[] Roles { get; } = ["user"];
        public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, CreatedPlayerDto>
        {
            private readonly PlayerBusinessRules _playerBusinessRules;
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public CreatePlayerCommandHandler(PlayerBusinessRules playerBusinessRules, IMapper mapper, IPlayerRepository playerRepository)
            {
                _playerBusinessRules = playerBusinessRules;
                _mapper = mapper;
                _playerRepository = playerRepository;
            }

            public async Task<CreatedPlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
            {
                await _playerBusinessRules.UserShouldBeExistsWhenPlayerCreated(request.UserId);
                await _playerBusinessRules.LobbyShouldBeExistsWhenPlayerCreated(request.LobbyId);

                Player player = _mapper.Map<Player>(request);
                Player addedPlayer = await _playerRepository.AddAsync(player);
                CreatedPlayerDto createdPlayerDto = _mapper.Map<CreatedPlayerDto>(addedPlayer);
                return createdPlayerDto;
            }
        }
    }
}
