using Application.Features.Chats.Models;
using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Queries.GetListChatByLobbyId
{
    public class GetListChatByLobbyIdQuery:IRequest<ChatListModel>,ISecuredRequest
    {
        public Guid LobbyId { get; set; }
        public string[] Roles { get; } = ["user"];


        public class GetListChatByLobbyIdQueryHandler : IRequestHandler<GetListChatByLobbyIdQuery, ChatListModel>
        {
            private readonly IChatRepository _chatRepository;
            private readonly IMapper _mapper;
            private readonly ChatBusinessRules _chatBusinessRules;

            public GetListChatByLobbyIdQueryHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules)
            {
                _chatRepository = chatRepository;
                _mapper = mapper;
                _chatBusinessRules = chatBusinessRules;
            }

            public async Task<ChatListModel> Handle(GetListChatByLobbyIdQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Chat> chats = await _chatRepository.GetListAsync(p => p.LobbyId == request.LobbyId,
                    include: m => m.Include(c => c.Player).ThenInclude(c => c.User),
                    orderBy:p=>p.OrderBy(c=>c.MessageDate));
              
                ChatListModel mappedModel = _mapper.Map<ChatListModel>(chats);

                return mappedModel;
            }
        }
    }
}
