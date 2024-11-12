﻿using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Queries.GetListLobby;
using Application.Features.Lobbies.Queries.GetListPublicLobby;
using Application.Features.Lobbies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
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
using static Application.Features.Lobbies.Queries.GetListLobby.GetListLobbyQuery;
using static Application.Features.Lobbies.Queries.GetListPublicLobby.GetListPublicLobbyQuery;

namespace VampireVillagers.Application.UnitTests.Features.Lobbies.Queries.GetListPublicLobbyTest
{
    public sealed class GetListPublicLobbyQueryUnitTest
    {
        private readonly Mock<ILobbyRepository> _lobbyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly LobbyBusinessRules _lobbyBusinessRules;

        public GetListPublicLobbyQueryUnitTest()
        {
            _lobbyRepository = new();
            _mapper = new();
            _lobbyBusinessRules = new(_lobbyRepository.Object);
        }

        [Fact]
        public async Task Lobby_WhenValidRequest_ShouldReturnLobbyListModel()
        {
            var query = new GetListPublicLobbyQuery()
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
                    }
                },
                Index = 0,
                Size = 10,
                Count = 1,
                Pages = 1,
                HasPrevious = false,
                HasNext = false
            });

            var handler = new GetListPublicLobbyQueryHandler( _lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var result = await handler.Handle(query, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<LobbyListModel>();
            result.Items.ShouldNotBeNull();
            result.Items.ShouldBeOfType<List<LobbyListDto>>();
            result.Items.Count.ShouldBe(result.Count);
        }
    }
}
