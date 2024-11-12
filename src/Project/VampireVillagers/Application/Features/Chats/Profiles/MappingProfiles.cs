using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Dtos;
using Application.Features.Chats.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateChatCommand, Chat>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Lobby, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore());

            CreateMap<Chat, CreatedChatDto>().ReverseMap();
            CreateMap<Chat, DeletedChatDto>().ReverseMap();

            CreateMap<Chat, GetListChatDto>()
                .ForMember(m => m.IsOwner, opt => opt.MapFrom(c => c.Player.IsOwner))
                .ForMember(m => m.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
                .ReverseMap();

            CreateMap<IPaginate<Chat>, ChatListModel>().ReverseMap();
        }
    }
}
