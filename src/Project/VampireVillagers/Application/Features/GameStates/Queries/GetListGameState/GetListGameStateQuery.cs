using Application.Features.GameStates.Models;
using Application.Features.GameStates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Queries.GetListGameState
{
    public class GetListGameStateQuery : IRequest<GameStateListModel>
    {
        public Guid GameSettingId { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListGameStateQueryHandler : IRequestHandler<GetListGameStateQuery, GameStateListModel>
        {
            private readonly IMapper _mapper;
            private readonly IGameStateRepository _gameStateRepository;
            private readonly GameStateBusinessRules _gameStateBusinessRules;

            public GetListGameStateQueryHandler(IMapper mapper, IGameStateRepository gameStateRepository, GameStateBusinessRules gameStateBusinessRules)
            {
                _mapper = mapper;
                _gameStateRepository = gameStateRepository;
                _gameStateBusinessRules = gameStateBusinessRules;
            }

            public async Task<GameStateListModel> Handle(GetListGameStateQuery request, CancellationToken cancellationToken)
            {
                IPaginate<GameState> gameStates = await _gameStateRepository.GetListAsync(p => p.GameSettingId == request.GameSettingId,
                                                                                            size: request.PageRequest.PageSize,
                                                                                            index: request.PageRequest.Page,
                                                                                            orderBy:m=>m.OrderByDescending(p=>p.LiveState),
                                                                                            include:m=>m.Include(c=>c.Player).ThenInclude(c=>c.User));

                GameStateListModel mappedModel = _mapper.Map<GameStateListModel>(gameStates);

                return mappedModel;
            }
        }
    }
}
