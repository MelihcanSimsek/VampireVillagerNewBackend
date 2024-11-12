using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Constants;
using Application.Features.Chats.Dtos;
using Application.Features.Chats.Profiles;
using Application.Features.Chats.Rules;
using Application.Services.LobbyService;
using Application.Services.PlayerService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using FluentValidation.TestHelper;
using Moq;
using Shouldly;
using static Application.Features.Chats.Commands.CreateChat.CreateChatCommand;

namespace VampireVillagers.Application.UnitTests.Features.Chats.Commands.CreateChatTest;

public sealed class CreateChatCommandUnitTest
{
    private readonly Mock<IChatRepository> _chatRepository;
    private readonly Mock<ILobbyService> _lobbyService;
    private readonly Mock<IPlayerService> _playerService;
    private readonly Mock<IMapper> _mapper;
    private readonly CreateChatCommandValidator _validator;
    public CreateChatCommandUnitTest()
    {
        _chatRepository = new();
        _lobbyService = new();
        _playerService = new();
        _mapper = new();
        _validator = new();
    }

    [Fact]
    public async Task Chat_WhenPlayerIdIsEmpty_ShouldGetValidationError()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.Empty,
            Message = "This is a new test message",
            MessageDate = DateTime.Now
        };

        var result = _validator.TestValidate(chatCommand);
        result.ShouldHaveValidationErrorFor(p => p.PlayerId);
    }

    [Fact]
    public async Task Chat_WhenLobbyIdIsEmpty_ShouldGetValidationError()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.Empty,
            PlayerId = Guid.NewGuid(),
            Message = "This is a new test message",
            MessageDate = DateTime.Now
        };

        var result = _validator.TestValidate(chatCommand);
        result.ShouldHaveValidationErrorFor(p => p.LobbyId);
    }

    [Fact]
    public async Task Chat_WhenMessageDateIsNull_ShouldGetValidatonError()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "This is a new test message",
            MessageDate = DateTime.MinValue
        };

        var result = _validator.TestValidate(chatCommand);
        result.ShouldHaveValidationErrorFor(p => p.MessageDate);
    }

    [Fact]
    public async Task Chat_WhenMessageIsEmpty_ShouldGetValidationError()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "",
            MessageDate = DateTime.Now
        };

        var result = _validator.TestValidate(chatCommand);
        result.ShouldHaveValidationErrorFor(p => p.Message);
    }

    [Fact]
    public async Task Chat_WhenAllPropertiesAreValid_ShouldNotHaveAnyErrors()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "This is a test message",
            MessageDate = DateTime.Now
        };

        var result = _validator.TestValidate(chatCommand);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task Chat_WhenLobbyNotFound_ShouldGetBussinesException()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "This is a new test message",
            MessageDate = DateTime.Now
        };

        _lobbyService
            .Setup(p => p.GetLobbyById(It.IsAny<Guid>()))
            .ReturnsAsync((Lobby)null);

        var chatBusinessRules = new ChatBusinessRules(_chatRepository.Object, _playerService.Object, _lobbyService.Object);
        var handler = new CreateChatCommandHandler(_chatRepository.Object, _mapper.Object, chatBusinessRules);

        var exception = await Should.ThrowAsync<BusinessException>(() => handler.Handle(chatCommand, CancellationToken.None));
        exception.Message.ShouldBe(Messages.LobbyNotFoundWhenAddingChat);
    }

    [Fact]
    public async Task Chat_WhenPlayerNotFound_ShouldGetBusinessException()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "This is a new test message",
            MessageDate = DateTime.Now
        };

        _lobbyService
          .Setup(p => p.GetLobbyById(It.IsAny<Guid>()))
          .ReturnsAsync(new Lobby());

        _playerService
            .Setup(p => p.GetPlayerById(It.IsAny<Guid>()))
            .ReturnsAsync((Player)null);

        var chatBusinessRules = new ChatBusinessRules(_chatRepository.Object, _playerService.Object, _lobbyService.Object);
        var handler = new CreateChatCommandHandler(_chatRepository.Object, _mapper.Object, chatBusinessRules);

        var excepiton = await Should.ThrowAsync<BusinessException>(() => handler.Handle(chatCommand, CancellationToken.None));
        excepiton.Message.ShouldBe(Messages.PlayerNotFoundWhenAddingChat);
    }

    [Fact]
    public async Task Chat_WhenValidRequest_ShouldReturnCreatedChatDto()
    {
        CreateChatCommand chatCommand = new CreateChatCommand()
        {
            LobbyId = Guid.NewGuid(),
            PlayerId = Guid.NewGuid(),
            Message = "This is a new test message",
            MessageDate = DateTime.Now
        };

        _lobbyService.Setup(p => p.GetLobbyById(It.IsAny<Guid>())).ReturnsAsync(new Lobby());

        _playerService.Setup(p => p.GetPlayerById(It.IsAny<Guid>())).ReturnsAsync(new Player());

        var chat = new Chat();
        _mapper.Setup(p => p.Map<Chat>(It.IsAny<CreateChatCommand>())).Returns(chat);
        _chatRepository.Setup(p => p.AddAsync(It.IsAny<Chat>())).ReturnsAsync(chat);

        var createdChatDto = new CreatedChatDto();
        _mapper.Setup(p => p.Map<CreatedChatDto>(It.IsAny<Chat>())).Returns(createdChatDto);

        var chatBusinessRules = new ChatBusinessRules(_chatRepository.Object, _playerService.Object, _lobbyService.Object);
        var handler = new CreateChatCommandHandler(_chatRepository.Object, _mapper.Object, chatBusinessRules);

        var result = await handler.Handle(chatCommand, CancellationToken.None);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<CreatedChatDto>();
    }


}
