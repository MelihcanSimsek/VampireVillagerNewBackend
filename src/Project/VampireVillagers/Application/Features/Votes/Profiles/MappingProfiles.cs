using Application.Features.Votes.Commands.CreateVote;
using Application.Features.Votes.Dtos;
using Application.Features.Votes.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateVoteCommand, Vote>().ReverseMap();
            CreateMap<Vote, CreatedVoteDto>()
                .ForMember(c => c.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
                .ForMember(c => c.TargetName, opt => opt.MapFrom(c => $"{c.Target.User.FirstName} {c.Target.User.LastName}"))
                .ReverseMap();

            CreateMap<Vote,UpdatedVoteDto>()
                .ForMember(c => c.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
                .ForMember(c => c.TargetName, opt => opt.MapFrom(c => $"{c.Target.User.FirstName} {c.Target.User.LastName}"))
                .ReverseMap();

            CreateMap<Vote, DeletedVoteDto>().ReverseMap();

            CreateMap<Vote, GetDayStateVoteListDto>()
                .ForMember(c => c.PlayerName, opt => opt.MapFrom(c => $"{c.Player.User.FirstName} {c.Player.User.LastName}"))
                .ReverseMap();

            CreateMap<IPaginate<Vote>, GetDayStateVoteListModel>().ReverseMap();
        }
    }
}
