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

namespace Application.Features.Chats.Commands.DeleteChat
{
    public class DeleteChatCommand:IRequest<DeletedChatDto>,ISecuredRequest
    {
        public Guid Id { get; set; }
        public string[] Roles { get; } = ["user"];

        public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, DeletedChatDto>
        {
            private readonly IChatRepository _chatRepository;
            private readonly IMapper _mapper;
            private readonly ChatBusinessRules _chatBusinessRules;

            public DeleteChatCommandHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules)
            {
                _chatRepository = chatRepository;
                _mapper = mapper;
                _chatBusinessRules = chatBusinessRules;
            }

            public async Task<DeletedChatDto> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
            {
                await _chatBusinessRules.ChatShouldBeExistsWhenDeleted(request.Id);

                Chat? chat = await _chatRepository.GetAsync(p => p.Id == request.Id);
                Chat deletedChat = await _chatRepository.DeleteAsync(chat);

                DeletedChatDto deletedChatDto = _mapper.Map<DeletedChatDto>(deletedChat);

                return deletedChatDto;
            }
        }
    }
}
