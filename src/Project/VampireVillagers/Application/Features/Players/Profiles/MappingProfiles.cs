using Application.Features.Players.Commands.CreatePlayer;
using Application.Features.Players.Dtos;
using Application.Features.Players.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Players.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Player, CreatePlayerCommand>().ReverseMap();
            CreateMap<Player, CreatedPlayerDto>().ForMember(c => c.LobbyName, opt => opt.MapFrom(c => c.Lobby.Name))
                .ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName)).ReverseMap();

            CreateMap<Player, DeletedPlayerDto>().ForMember(c => c.LobbyName, opt => opt.MapFrom(c => c.Lobby.Name))
             .ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName)).ReverseMap();

            CreateMap<Player, PlayerGetListDto>()
                .ForMember(c => c.UserName, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName)).ReverseMap();
            CreateMap<IPaginate<Player>, PlayerListModel>().ReverseMap();
                

        }
    }
}
