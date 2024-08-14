using Application.Features.Lobbies.Commands.CreateLobby;
using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Lobbies.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateLobbyCommand, Lobby>().ReverseMap();
            CreateMap<Lobby, CreatedLobbyDto>().ReverseMap();

            CreateMap<Lobby, DeletedLobbyDto>().ReverseMap();

            CreateMap<Lobby, LobbyListDto>().ReverseMap();
            CreateMap<IPaginate<Lobby>, LobbyListModel>().ReverseMap();
        }
    }
}
