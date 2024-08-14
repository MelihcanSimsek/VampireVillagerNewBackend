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

namespace Application.Features.Lobbies.Commands.DeleteLobby
{
    public class DeleteLobbyCommand:IRequest<DeletedLobbyDto>,ISecuredRequest
    {
        public Guid Id { get; set; }
        public string[] Roles { get; } = ["user"];
        public class DeleteLobbyCommandHandler : IRequestHandler<DeleteLobbyCommand, DeletedLobbyDto>
        {
            private readonly ILobbyRepository _lobbyRepository;
            private readonly IMapper _mapper;
            private readonly LobbyBusinessRules _lobbyBusinessRules;
            public DeleteLobbyCommandHandler(ILobbyRepository lobbyRepository, IMapper mapper, LobbyBusinessRules lobbyBusinessRules)
            {
                _lobbyRepository = lobbyRepository;
                _mapper = mapper;
                _lobbyBusinessRules = lobbyBusinessRules;
            }

            public async Task<DeletedLobbyDto> Handle(DeleteLobbyCommand request, CancellationToken cancellationToken)
            {
                await _lobbyBusinessRules.LobbyShouldExistsWhenDeleted(request.Id);

                Lobby? lobby =await _lobbyRepository.GetAsync(p => p.Id == request.Id);
                Lobby deletedLobby = await _lobbyRepository.DeleteAsync(lobby);
                DeletedLobbyDto deletedLobbyDto = _mapper.Map<DeletedLobbyDto>(deletedLobby);

                return deletedLobbyDto;
            }
        }
    }
}
