using Application.Features.GameSettings.Dtos;
using Application.Features.GameSettings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Commands.DeleteGameSetting
{
    public class DeleteGameSettingCommand:IRequest<DeletedGameSettingDto>
    {
        public Guid Id { get; set; }

        public class DeleteGameSettingCommandHandler : IRequestHandler<DeleteGameSettingCommand, DeletedGameSettingDto>
        {
            private readonly IGameSettingRepository _gameSettingRepository;
            private readonly IMapper _mapper;
            private readonly GameSettingBusinessRules _gameSettingBusinessRules;

            public DeleteGameSettingCommandHandler(IGameSettingRepository gameSettingRepository, IMapper mapper, GameSettingBusinessRules gameSettingBusinessRules)
            {
                _gameSettingRepository = gameSettingRepository;
                _mapper = mapper;
                _gameSettingBusinessRules = gameSettingBusinessRules;
            }

            public async Task<DeletedGameSettingDto> Handle(DeleteGameSettingCommand request, CancellationToken cancellationToken)
            {
                await _gameSettingBusinessRules.GameSettingShouldBeExistsWhenDeleted(request.Id);

                GameSetting? gameSetting = await _gameSettingRepository.GetAsync(p => p.Id == request.Id);
                GameSetting deletedGameSetting = await _gameSettingRepository.DeleteAsync(gameSetting);
                DeletedGameSettingDto deletedGameSettingDto = _mapper.Map<DeletedGameSettingDto>(deletedGameSetting);
                return deletedGameSettingDto;
            }

        }
    }
}
