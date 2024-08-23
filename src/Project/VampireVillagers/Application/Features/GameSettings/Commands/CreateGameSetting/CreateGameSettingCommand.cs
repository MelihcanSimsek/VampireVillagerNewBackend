﻿using Application.Features.GameSettings.Dtos;
using Application.Features.GameSettings.Rules;
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

namespace Application.Features.GameSettings.Commands.CreateGameSetting
{
    public class CreateGameSettingCommand:IRequest<CreatedGameSettingDto>,ISecuredRequest
    {
        public Guid LobbyId { get; set; }
        public DateTime CreationDate { get; set; }
        public int NightTime { get; set; }
        public int DayTime { get; set; }
        public int VampireNumber { get; set; }
        public int PriestNumber { get; set; }
        public int WitchNumber { get; set; }
        public int VampireHunterNumber { get; set; }
        public int ShapeshifterNumber { get; set; }
        public int TransformingVampireNumber { get; set; }
        public string[] Roles { get; } = ["user"];


        public class CreateGameSettingCommandHandler : IRequestHandler<CreateGameSettingCommand, CreatedGameSettingDto>
        {
            private readonly IGameSettingRepository _gameSettingRepository;
            private readonly IMapper _mapper;
            private readonly GameSettingBusinessRules _gameSettingBusinessRules;
            public CreateGameSettingCommandHandler(IGameSettingRepository gameSettingRepository, IMapper mapper, GameSettingBusinessRules gameSettingBusinessRules)
            {
                _gameSettingRepository = gameSettingRepository;
                _mapper = mapper;
                _gameSettingBusinessRules = gameSettingBusinessRules;
            }

            public async Task<CreatedGameSettingDto> Handle(CreateGameSettingCommand request, CancellationToken cancellationToken)
            {
                await _gameSettingBusinessRules.LobbyShouldBeExistsWhenGameSettingAdded(request.LobbyId);

                GameSetting gameSetting = _mapper.Map<GameSetting>(request);
                GameSetting addedGameSetting =await _gameSettingRepository.AddAsync(gameSetting);
                CreatedGameSettingDto createdGameSettingDto = _mapper.Map<CreatedGameSettingDto>(addedGameSetting);
                return createdGameSettingDto;
            }
        }
    }
}