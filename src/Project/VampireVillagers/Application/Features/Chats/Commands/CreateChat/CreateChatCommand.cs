using Application.Features.Chats.Dtos;
using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Commands.CreateChat
{
    public class CreateChatCommand:IRequest<CreatedChatDto>,ISecuredRequest
    {
        public Guid LobbyId { get; set; }
        public Guid PlayerId { get; set; }
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public string[] Roles { get; } = ["user"];

        public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, CreatedChatDto>
        {
            private readonly IChatRepository _chatRepository;
            private readonly IMapper _mapper;
            private readonly ChatBusinessRules _chatBusinessRules;

            public CreateChatCommandHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules)
            {
                _chatRepository = chatRepository;
                _mapper = mapper;
                _chatBusinessRules = chatBusinessRules;
            }

            public async Task<CreatedChatDto> Handle(CreateChatCommand request, CancellationToken cancellationToken)
            {
                await _chatBusinessRules.LobbyShouldBeExistsWhenChatCreated(request.LobbyId);
                await _chatBusinessRules.PlayerShouldBeExistsWhenChatCreated(request.PlayerId);

                Chat chat = _mapper.Map<Chat>(request);
                Chat addedChat = await _chatRepository.AddAsync(chat);
                CreatedChatDto createdChatDto = _mapper.Map<CreatedChatDto>(addedChat);

                return createdChatDto;
            }
        }

    }
}
