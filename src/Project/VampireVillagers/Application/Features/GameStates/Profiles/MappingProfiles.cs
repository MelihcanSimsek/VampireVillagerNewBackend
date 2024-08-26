using Application.Features.GameStates.Dtos;
using Application.Features.GameStates.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GameStates.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<GameState, GetListGameStateDto>()
                .ForMember(c => c.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
                .ReverseMap();

            CreateMap<IPaginate<GameState>, GameStateListModel>().ReverseMap();

            CreateMap<GameState, GetGameStateDto>()
              .ForMember(c => c.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
              .ReverseMap();
        }
    }
}
