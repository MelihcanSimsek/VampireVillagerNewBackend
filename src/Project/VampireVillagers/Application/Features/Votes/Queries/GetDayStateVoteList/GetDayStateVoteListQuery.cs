using Application.Features.Votes.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.Votes.Queries.GetDayStateVoteList
{
    public class GetDayStateVoteListQuery:IRequest<GetDayStateVoteListModel>,ISecuredRequest
    {
        public Guid GameSettingId { get; set; }
        public int Day { get; set; }
        public PageRequest PageRequest { get; set; }

        public string[] Roles { get; } = ["user"];

        public class GetDayStateVoteListQueryHandler : IRequestHandler<GetDayStateVoteListQuery, GetDayStateVoteListModel>
        {
            private readonly IMapper _mapper;
            private readonly IVoteRepository _voteRepository;

            public GetDayStateVoteListQueryHandler(IMapper mapper, IVoteRepository voteRepository)
            {
                _mapper = mapper;
                _voteRepository = voteRepository;
            }

            public async Task<GetDayStateVoteListModel> Handle(GetDayStateVoteListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Vote> votes = await _voteRepository.GetListAsync(p => p.DayType == false
                                                                        && p.Day == request.Day
                                                                        && p.GameSettingId == request.GameSettingId,
                                                                        include: m => m.Include(c => c.Player).ThenInclude(c => c.User),
                                                                        index:request.PageRequest.Page,
                                                                        size:request.PageRequest.PageSize);

                GetDayStateVoteListModel mappedModel = _mapper.Map<GetDayStateVoteListModel>(votes);

                return mappedModel;
            }
        }
    }
}
