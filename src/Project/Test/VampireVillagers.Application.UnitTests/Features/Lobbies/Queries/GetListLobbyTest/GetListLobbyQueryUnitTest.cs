
using Application.Features.Lobbies.Queries.GetListLobby;
using Application.Features.Lobbies.Rules;
using Application.Services.Repositories;
using Core.Application.Requests;
using AutoMapper;
using Moq;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Dtos;
using static Application.Features.Lobbies.Queries.GetListLobby.GetListLobbyQuery;
using Shouldly;

namespace VampireVillagers.Application.UnitTests.Features.Lobbies.Queries.GetListLobbyTest
{
    public sealed class GetListLobbyQueryUnitTest
    {
        private readonly Mock<ILobbyRepository> _lobbyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly LobbyBusinessRules _lobbyBusinessRules;

        public GetListLobbyQueryUnitTest()
        {
            _lobbyRepository = new();
            _mapper = new();
            _lobbyBusinessRules = new(_lobbyRepository.Object);
        }

        [Fact]
        public async Task Lobby_WhenValidRequest_ShouldReturnLobbyListModel()
        {
            var query = new GetListLobbyQuery()
            {
                PageRequest = new PageRequest() { Page = 0, PageSize = 10 }
            };

            Mock<IPaginate<Lobby>> pagianeteMock = new();
            _lobbyRepository.Setup(p => p.GetListAsync(
                It.IsAny<Expression<Func<Lobby, bool>>>(),
                It.IsAny<Func<IQueryable<Lobby>, IOrderedQueryable<Lobby>>>(),
                It.IsAny<Func<IQueryable<Lobby>, IIncludableQueryable<Lobby, object>>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>())).ReturnsAsync(pagianeteMock.Object);
            _mapper.Setup(p => p.Map<LobbyListModel>(It.IsAny<IPaginate<Lobby>>())).Returns(new LobbyListModel
            {
                Items = new List<LobbyListDto>()
                {
                    new LobbyListDto()
                    {
                        Id= Guid.NewGuid(),
                        CreationDate= DateTime.Now,
                        HasPassword= false,
                        Name= "This is a public test lobby"
                    },
                    new LobbyListDto()
                    {
                        Id= Guid.NewGuid(),
                        CreationDate= DateTime.Now,
                        HasPassword= true,
                        Name= "This is a private test lobby"
                    }
                },
                Index = 0,
                Size = 10,
                Count = 2,
                Pages = 1,
                HasPrevious = false,
                HasNext = false
            });

            var handler = new GetListLobbyQueryHandler(_mapper.Object, _lobbyRepository.Object, _lobbyBusinessRules);
            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<LobbyListModel>();
            result.Items.ShouldNotBeNull();
            result.Items.ShouldBeOfType<List<LobbyListDto>>();
            result.Items.Count.ShouldBe(result.Count);
        }

    }
}
