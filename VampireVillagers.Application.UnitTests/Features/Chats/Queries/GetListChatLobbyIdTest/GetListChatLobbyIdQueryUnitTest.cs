using Application.Features.Chats.Dtos;
using Application.Features.Chats.Models;
using Application.Features.Chats.Queries.GetListChatByLobbyId;
using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Chats.Queries.GetListChatByLobbyId.GetListChatByLobbyIdQuery;

namespace VampireVillagers.Application.UnitTests.Features.Chats.Queries.GetListChatLobbyIdTest
{
    public sealed class GetListChatLobbyIdQueryUnitTest
    {
        private readonly Mock<IChatRepository> _chatRepository;
        private readonly Mock<IMapper> _mapper;

        public GetListChatLobbyIdQueryUnitTest()
        {
            _chatRepository = new();
            _mapper = new();
        }

        [Fact]
        public async Task Chat_WhenLobbyIdExists_ShouldReturnChatListModel()
        {
            var query = new GetListChatByLobbyIdQuery()
            {
                LobbyId = Guid.NewGuid()
            };

            Mock<IPaginate<Chat>> paginateChats = new();

            _chatRepository.Setup(p => p.GetListAsync(
                It.IsAny<Expression<Func<Chat, bool>>>(),
                It.IsAny<Func<IQueryable<Chat>, IOrderedQueryable<Chat>>>(),
                It.IsAny<Func<IQueryable<Chat>, IIncludableQueryable<Chat, object>>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(paginateChats.Object);


            var expectedChatListModel = new ChatListModel()
            {
                Items = new List<GetListChatDto>
                {
                    new GetListChatDto
                    {
                        Id = Guid.NewGuid(),
                        PlayerId = Guid.NewGuid(),
                        PlayerName = "Test Player",
                        IsOwner = true,
                        Message = "Test message",
                        MessageDate = DateTime.UtcNow
                    }
                },
                Index = 0,
                Size = 10,
                Count = 1,
                Pages = 1,
                HasPrevious = false,
                HasNext = false
            };

            _mapper.Setup(p => p.Map<ChatListModel>(It.IsAny<IPaginate<Chat>>())).Returns(expectedChatListModel);


            var handler = new GetListChatByLobbyIdQueryHandler(_chatRepository.Object,
                _mapper.Object, new ChatBusinessRules(default, default, default));

            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
        }
    }
}
