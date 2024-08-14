using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Rules;
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

namespace Application.Features.Lobbies.Commands.CreateLobby
{
    public class CreateLobbyCommand : IRequest<CreatedLobbyDto>, ISecuredRequest
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool HasPassword { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; } = ["user"];

        public class CreateLobbyCommandHandler : IRequestHandler<CreateLobbyCommand, CreatedLobbyDto>
        {
            private readonly ILobbyRepository _lobbyRepository;
            private readonly IMapper _mapper;
            private readonly LobbyBusinessRules _lobbyBusinessRules;

            public CreateLobbyCommandHandler(ILobbyRepository lobbyRepository, IMapper mapper, LobbyBusinessRules lobbyBusinessRules)
            {
                _lobbyRepository = lobbyRepository;
                _mapper = mapper;
                _lobbyBusinessRules = lobbyBusinessRules;
            }

            public async Task<CreatedLobbyDto> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
            {
                await _lobbyBusinessRules.LobbyNameCanNotBeDuplicated(request.Name);
                await _lobbyBusinessRules.CheckLobbyPasswordNotEmptyWhenLobbyLocked(request.Password, request.HasPassword);

                Lobby lobby = _mapper.Map<Lobby>(request);
                Lobby addedLobby = await _lobbyRepository.AddAsync(lobby);
                CreatedLobbyDto createdLobbyDto = _mapper.Map<CreatedLobbyDto>(addedLobby);

                return createdLobbyDto;
            }
        }
    }
}
