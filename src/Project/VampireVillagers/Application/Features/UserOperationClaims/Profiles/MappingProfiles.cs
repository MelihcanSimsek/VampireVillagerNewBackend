using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, CreatedUserOperationClaimDto>().ForMember(c => c.Role, opt => opt.MapFrom(c => c.OperationClaim.Name))
                .ForMember(c => c.Username, opt => opt.MapFrom(c => c.User.FirstName + " " + c.User.LastName))
                .ForMember(c => c.Email, opt => opt.MapFrom(c => c.User.Email)).ReverseMap();
        }
    }
}
