using Application.Features.Players.Dtos;
using Application.Features.Players.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Queries.GetListPlayerByLobbyId
{
    public class GetListPlayerByLobbyIdQuery:IRequest<PlayerListModel>,ISecuredRequest
    {
        public Guid LobbyId { get; set; }
        public string[] Roles { get; } = ["user"];
        public class GetListPlayerByLobbyIdQueryHandler : IRequestHandler<GetListPlayerByLobbyIdQuery, PlayerListModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public GetListPlayerByLobbyIdQueryHandler(IMapper mapper, IPlayerRepository playerRepository)
            {
                _mapper = mapper;
                _playerRepository = playerRepository;
            }

            public async Task<PlayerListModel> Handle(GetListPlayerByLobbyIdQuery request, CancellationToken cancellationToken)
            {
                int index = 0;
                int playerAmount = 12;

                IPaginate<Player> playerList = await _playerRepository.GetListAsync(p => p.LobbyId == request.LobbyId,index:index,size:playerAmount);

                PlayerListModel mappedModel = _mapper.Map<PlayerListModel>(playerList);

                return mappedModel;
            }
        }
    }
}
