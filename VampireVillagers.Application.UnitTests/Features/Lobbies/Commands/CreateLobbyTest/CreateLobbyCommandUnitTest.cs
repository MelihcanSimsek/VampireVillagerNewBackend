using Application.Features.Lobbies.Commands.CreateLobby;
using Application.Features.Lobbies.Constants;
using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Lobbies.Commands.CreateLobby.CreateLobbyCommand;

namespace VampireVillagers.Application.UnitTests.Features.Lobbies.Commands.CreateLobbyTest
{
    public sealed class CreateLobbyCommandUnitTest
    {
        private readonly Mock<ILobbyRepository> _lobbyRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly LobbyBusinessRules _lobbyBusinessRules;
        private readonly CreateLobbyCommandValidation _validator;

        public CreateLobbyCommandUnitTest()
        {
            _lobbyRepository = new();
            _mapper = new();
            _lobbyBusinessRules = new LobbyBusinessRules(_lobbyRepository.Object);
            _validator = new();
        }

        [Fact]
        public async Task Lobby_WhenNameIsEmpty_ShouldGetValidationError()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.Now,
                HasPassword = false,
                Password = "",
                Name = ""
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact]
        public async Task Lobby_WhenCreationDateIsNotValid_ShouldGetValidationError()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.MinValue,
                HasPassword = false,
                Password = "",
                Name = "This is a test lobby"
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(p => p.CreationDate);
        }

        [Fact]
        public async Task Lobby_WhenLobbyNameDuplicated_ShouldGetBusinessException()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.Now,
                HasPassword = false,
                Password = "",
                Name = "This is a test lobby"
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync(new Lobby());

            var handler = new CreateLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var exception = await Should.ThrowAsync<BusinessException>(() => handler.Handle(command,CancellationToken.None));

            exception.Message.ShouldBe(Messages.LobbyNameAlreadyExists);

        }

        [Fact]
        public async Task Lobby_WhenLobbyLockedAndPasswordEmpty_ShouldGetBusinessException()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "  ",
                Name = "This is a test lobby"
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync((Lobby)null);

            var handler = new CreateLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var exception = await Should.ThrowAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));

            exception.Message.ShouldBe(Messages.LobbyPasswordCanNotBeBlank);
        }

        [Fact]
        public async Task Lobby_WhenLobbyUnlockedAndValidRequest_ShouldGetCreatedLobbyDto()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.Now,
                HasPassword = false,
                Password = "",
                Name = "This is a test lobby"
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync((Lobby)null);
            _lobbyRepository.Setup(p => p.AddAsync(It.IsAny<Lobby>())).ReturnsAsync(new Lobby());

            _mapper.Setup(p => p.Map<Lobby>(It.IsAny<CreateLobbyCommand>())).Returns(new Lobby());
            _mapper.Setup(p => p.Map<CreatedLobbyDto>(It.IsAny<Lobby>())).Returns(new CreatedLobbyDto());

            var handler = new CreateLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<CreatedLobbyDto>();
        }

        [Fact]
        public async Task Lobby_WhenLobbyLockedAndValidRequest_ShouldGetCreatedLobbyDto()
        {
            var command = new CreateLobbyCommand()
            {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "asdasd",
                Name = "This is a test lobby"
            };

            _lobbyRepository.Setup(p => p.GetAsync(It.IsAny<Expression<Func<Lobby, bool>>>())).ReturnsAsync((Lobby)null);
            _lobbyRepository.Setup(p => p.AddAsync(It.IsAny<Lobby>())).ReturnsAsync(new Lobby());

            _mapper.Setup(p => p.Map<Lobby>(It.IsAny<CreateLobbyCommand>())).Returns(new Lobby());
            _mapper.Setup(p => p.Map<CreatedLobbyDto>(It.IsAny<Lobby>())).Returns(new CreatedLobbyDto());

            var handler = new CreateLobbyCommandHandler(_lobbyRepository.Object, _mapper.Object, _lobbyBusinessRules);
            var result = await handler.Handle(command, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<CreatedLobbyDto>();
        }

    }
}
