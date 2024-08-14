using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Queries.GetListPublicLobby
{
    public class GetListPublicLobbyQuery : IRequest<LobbyListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListPublicLobbyQueryHandler : IRequestHandler<GetListPublicLobbyQuery, LobbyListModel>
        {
            private readonly ILobbyRepository _lobbyRepository;
            private readonly IMapper _mapper;
            private readonly LobbyBusinessRules _lobbyBusinessRules;

            public GetListPublicLobbyQueryHandler(ILobbyRepository lobbyRepository, IMapper mapper, LobbyBusinessRules lobbyBusinessRules)
            {
                _lobbyRepository = lobbyRepository;
                _mapper = mapper;
                _lobbyBusinessRules = lobbyBusinessRules;
            }

            public async Task<LobbyListModel> Handle(GetListPublicLobbyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Lobby> lobbies = await _lobbyRepository.GetListAsync(p => p.HasPassword == false && p.CreationDate > DateTime.UtcNow.AddHours(-2), size: request.PageRequest.PageSize, index: request.PageRequest.Page);
                LobbyListModel mappedModel = _mapper.Map<LobbyListModel>(lobbies);
                return mappedModel;
            }
        }
    }
}
