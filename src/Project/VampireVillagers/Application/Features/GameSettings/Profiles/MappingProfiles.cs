using Application.Features.GameSettings.Commands.CreateGameSetting;
using Application.Features.GameSettings.Dtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameSettings.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<GameSetting, CreateGameSettingCommand>().ReverseMap();
            CreateMap<GameSetting, CreatedGameSettingDto>().ReverseMap();
            CreateMap<GameSetting, DeletedGameSettingDto>().ReverseMap();
            CreateMap<GameSetting, GetGameSettingByLobbyIdDto>().ReverseMap();
        }
    }
}
