using Application.Features.GameStates.Models;
using Application.Features.GameStates.Rules;
using Application.Services.Repositories;
using Application.Services.VoteService;
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

namespace Application.Features.GameStates.Commands.EndDay
{
    public class EndDayCommand : IRequest<DayListModel>
    {
        public Guid GameSettingId { get; set; }
        public int Day { get; set; }
        public PageRequest PageRequest { get; set; }

        public class EndDayCommandHandler : IRequestHandler<EndDayCommand, DayListModel>
        {
            private readonly IGameStateRepository _gameStateRepository;
            private readonly IMapper _mapper;
            private readonly GameStateBusinessRules _gameStateBusinessRules;
            private readonly IVoteService _voteManager;

            public EndDayCommandHandler(IGameStateRepository gameStateRepository, IMapper mapper, GameStateBusinessRules gameStateBusinessRules, IVoteService voteManager)
            {
                _gameStateRepository = gameStateRepository;
                _mapper = mapper;
                _gameStateBusinessRules = gameStateBusinessRules;
                _voteManager = voteManager;
            }

            public async Task<DayListModel> Handle(EndDayCommand request, CancellationToken cancellationToken)
            {
                Player? hangedPlayer = await _voteManager.GetHangedPlayer(request.GameSettingId, request.Day, false);

                await ChangeLiveStateHangedPlayer(hangedPlayer);

                IPaginate<GameState> gameStates = await _gameStateRepository.GetListAsync(p => p.GameSettingId == request.GameSettingId,
                                                                            size: request.PageRequest.PageSize,
                                                                            index: request.PageRequest.Page,
                                                                            orderBy: m => m.OrderByDescending(p => p.LiveState),
                                                                            include: m => m.Include(c => c.Player).ThenInclude(c => c.User));

                DayListModel mappedModel = _mapper.Map<DayListModel>(gameStates);
                return mappedModel;
            }


            private async Task ChangeLiveStateHangedPlayer(Player? player)
            {
                if (player != null)
                {
                    GameState gameState = _mapper.Map<GameState>(player);
                    gameState.LiveState = false;
                    await _gameStateRepository.UpdateAsync(gameState);
                }
            }
        }
    }
}
