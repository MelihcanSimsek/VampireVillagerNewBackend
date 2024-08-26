using Application.Features.GameStates.Dtos;
using Application.Features.GameStates.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Queries.GetGameState
{
    public class GetGameStateQuery:IRequest<GetGameStateDto>
    {
        public Guid PlayerId { get; set; }
        public Guid GameSettingId { get; set; }

        public class GetGameStateQueryHandler : IRequestHandler<GetGameStateQuery, GetGameStateDto>
        {
            private readonly IMapper _mapper;
            private readonly IGameStateRepository _gameStateRepository;
            private readonly GameStateBusinessRules _gameStateBusinessRules;

            public GetGameStateQueryHandler(IMapper mapper, IGameStateRepository gameStateRepository, GameStateBusinessRules gameStateBusinessRules)
            {
                _mapper = mapper;
                _gameStateRepository = gameStateRepository;
                _gameStateBusinessRules = gameStateBusinessRules;
            }

            public async Task<GetGameStateDto> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
            {
                GameState? gameState = (await _gameStateRepository.GetListAsync(p => p.GameSettingId == request.GameSettingId && p.PlayerId == request.PlayerId)).Items.FirstOrDefault();

                GetGameStateDto getGameStateDto = _mapper.Map<GetGameStateDto>(gameState);

                return getGameStateDto;
            }
        }
    }
}
