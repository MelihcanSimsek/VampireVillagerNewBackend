using Application.Features.Lobbies.Commands.DeleteLobby;
using Application.Features.Lobbies.Constants;
using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Rules;
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
using static Application.Features.Lobbies.Commands.DeleteLobby.DeleteLobbyCommand;

namespace VampireVillagers.Application.UnitTests.Features.Lobbies.Commands.DeleteLobbyTest
{
    public sealed class DeleteLobbyCommandUnitTest
    {
        private readonly Mock<ILobbyRepository> _lobbyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly LobbyBusinessRules _lobbyBusinessRules;

        public DeleteLobbyCommandUnitTest()
        {
            _lobbyRepository = new();
            _mapper = new();
            _lobbyBusinessRules = new(_lobbyRepository.Object);
        }

        [Fact]
        public async Task Lobby_WhenLobbyNotFound_ShouldGetBusinessExcepiton()
        {
            var command = new DeleteLobbyCommand()
            {
                Id = Guid.NewGuid()
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync((Lobby)null);

            var handler = new DeleteLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var exception = await Should.ThrowAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));

            exception.Message.ShouldBe(Messages.LobbyDoesNotExists);
        }

        [Fact]
        public async Task Lobby_WhenValidRequest_ShouldReturnDeletedLobbyDto()
        {
            var command = new DeleteLobbyCommand()
            {
                Id = Guid.NewGuid()
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync(new Lobby());
            _lobbyRepository.Setup(p => p.DeleteAsync(It.IsAny<Lobby>())).ReturnsAsync(new Lobby());
            _mapper.Setup(p => p.Map<DeletedLobbyDto>(It.IsAny<Lobby>())).Returns(new DeletedLobbyDto());

            var handler = new DeleteLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<DeletedLobbyDto>();
        }
    }
}
