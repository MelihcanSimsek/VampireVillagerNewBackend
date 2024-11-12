using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Commands.DeleteChat;
using Application.Features.Chats.Constants;
using Application.Features.Chats.Dtos;
using Application.Features.Chats.Rules;
using Application.Services.LobbyService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Chats.Commands.DeleteChat.DeleteChatCommand;

namespace VampireVillagers.Application.UnitTests.Features.Chats.Commands.DeleteChatTest
{
    public sealed class DeleteChatCommandUnitTest
    {
        private readonly Mock<IChatRepository> _chatRepository;
        private readonly Mock<ILobbyService> _lobbyService;
        private readonly Mock<IPlayerService> _playerService;
        private readonly Mock<IMapper> _mapper;
        public DeleteChatCommandUnitTest()
        {
            _chatRepository = new();
            _lobbyService = new();
            _playerService = new();
            _mapper = new();
        }

        [Fact]
        public async Task Chat_WhenChatNotFound_ShouldGetBusinessException()
        {
            DeleteChatCommand command = new DeleteChatCommand()
            {
                Id = Guid.NewGuid()
            };

            _chatRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>())).ReturnsAsync((Chat)null);
            var chatBusinessRules = new ChatBusinessRules(_chatRepository.Object, _playerService.Object, _lobbyService.Object);
            var handler = new DeleteChatCommandHandler(_chatRepository.Object, _mapper.Object, chatBusinessRules);

            var exception = await Should.ThrowAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
            exception.Message.ShouldBe(Messages.ChatNotFoundWhenDeleted);
        }

        [Fact]
        public async Task Chat_WhenValidRequest_ShouldReturnDeletedChatDto()
        {
            DeleteChatCommand command = new DeleteChatCommand()
            {
                Id = Guid.NewGuid()
            };
            
            _chatRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Chat, bool>>>())).ReturnsAsync(new Chat());
            _chatRepository.Setup(p => p.AddAsync(It.IsAny<Chat>())).ReturnsAsync(new Chat());
            _mapper.Setup(p => p.Map<DeletedChatDto>(It.IsAny<Chat>())).Returns(new DeletedChatDto());

            var chatBusinessRules = new ChatBusinessRules(_chatRepository.Object, _playerService.Object, _lobbyService.Object);
            var handler = new DeleteChatCommandHandler(_chatRepository.Object, _mapper.Object, chatBusinessRules);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<DeletedChatDto>();
        }

    }
}
