using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Queries.GetListLobby
{
    public class GetListLobbyQuery:IRequest<LobbyListModel>,ISecuredRequest
    {
        public PageRequest PageRequest { get; set; }
        public string[] Roles { get; } = ["user"];
        public class GetListLobbyQueryHandler : IRequestHandler<GetListLobbyQuery, LobbyListModel>
        {
            private readonly IMapper _mapper;
            private readonly ILobbyRepository _lobbyRepository;
            private readonly LobbyBusinessRules _lobbyBusinessRules;

            public GetListLobbyQueryHandler(IMapper mapper, ILobbyRepository lobbyRepository, LobbyBusinessRules lobbyBusinessRules)
            {
                _mapper = mapper;
                _lobbyRepository = lobbyRepository;
                _lobbyBusinessRules = lobbyBusinessRules;
            }

            public async Task<LobbyListModel> Handle(GetListLobbyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Lobby> lobbies = await _lobbyRepository.GetListAsync(p=>p.CreationDate > DateTime.UtcNow.AddHours(-2),size: request.PageRequest.PageSize, index: request.PageRequest.Page);

                LobbyListModel mappedModel = _mapper.Map<LobbyListModel>(lobbies);

                return mappedModel;
            }
        }
    }
}
