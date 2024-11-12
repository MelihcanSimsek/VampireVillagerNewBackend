using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Dtos;
using Application.Features.Chats.Models;
using Application.Features.Chats.Profiles;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireVillagers.Application.UnitTests.Features.Chats.Profiles
{
    public sealed class MappingProfilesUnitTest
    {
        private readonly IMapper _mapper;
        public MappingProfilesUnitTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Mapper_MappingConfiguration_IsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void Mapper_Should_Map_CreateChatCommand_To_Chat()
        {
            var command = new CreateChatCommand()
            {
                LobbyId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Message = "This is a test message",
                MessageDate = DateTime.Now
            };

            var result = _mapper.Map<Chat>(command);

            result.ShouldNotBeNull();
            result.LobbyId.ShouldBe(command.LobbyId);
            result.PlayerId.ShouldBe(command.PlayerId);
            result.Message.ShouldBe(command.Message);
            result.MessageDate.ShouldBe(command.MessageDate);
        }

        [Fact]
        public void Mapper_Should_Map_Chat_To_CreatedChatDto()
        {
            var chat = new Chat()
            {
                Id = Guid.NewGuid(),
                LobbyId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Message = "This is a test message",
                MessageDate = DateTime.Now,
            };

            var result = _mapper.Map<CreatedChatDto>(chat);

            result.ShouldNotBeNull();
            result.PlayerId.ShouldBe(chat.PlayerId);
            result.LobbyId.ShouldBe(chat.LobbyId);
            result.Message.ShouldBe(chat.Message);
            result.MessageDate.ShouldBe(chat.MessageDate);
            result.Id.ShouldBe(chat.Id);
        }

        [Fact]
        public void Mapper_Should_Map_Chat_To_DeletedChatDto()
        {
            var chat = new Chat()
            {
                Id = Guid.NewGuid(),
                LobbyId = Guid.NewGuid(),
                PlayerId = Guid.NewGuid(),
                Message = "This is a test message",
                MessageDate = DateTime.Now,
            };

            var result = _mapper.Map<DeletedChatDto>(chat);

            result.ShouldNotBeNull();
            result.PlayerId.ShouldBe(chat.PlayerId);
            result.LobbyId.ShouldBe(chat.LobbyId);
            result.Message.ShouldBe(chat.Message);
            result.MessageDate.ShouldBe(chat.MessageDate);
            result.Id.ShouldBe(chat.Id);
        }

        [Fact]
        public void Mapper_Should_Map_Chat_To_GetListChatDto()
        {
            var playerId = Guid.NewGuid();
            var lobbyId = Guid.NewGuid();
            var chat = new Chat()
            {
                Id = Guid.NewGuid(),
                LobbyId = lobbyId,
                PlayerId = playerId,
                Message = "This is a test message",
                MessageDate = DateTime.Now,
                Player = new Player {
                    Id = playerId,
                    IsOwner = true,
                    LobbyId = lobbyId,
                    UserId = Guid.NewGuid(),
                    User = new User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "test@test.com",
                        FirstName = "Jon",
                        LastName = "Snow",
                        Status = false
                }
                },
                Lobby= new Lobby
                {
                    Id= lobbyId,
                    CreationDate = DateTime.Now,
                    Name = "This is a test lobby",
                    HasPassword=false,
                    Password="",
                }
            };

            var result = _mapper.Map<GetListChatDto>(chat);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(chat.Id);
            result.IsOwner.ShouldBe(chat.Player.IsOwner);
            result.Message.ShouldBe(chat.Message);
            result.MessageDate.ShouldBe(chat.MessageDate);
            result.PlayerId.ShouldBe(playerId);
            result.PlayerName.ShouldBe(chat.Player.User.FirstName + " " + chat.Player.User.LastName);
        }

        [Fact]
        public void Mapper_Should_Map_IPaginateChat_To_ChatListModel()
        {
            var playerId = Guid.NewGuid();
            var lobbyId = Guid.NewGuid();
            var chat = new Chat()
            {
                Id = Guid.NewGuid(),
                LobbyId = lobbyId,
                PlayerId = playerId,
                Message = "This is a test message",
                MessageDate = DateTime.Now,
                Player = new Player
                {
                    Id = playerId,
                    IsOwner = true,
                    LobbyId = lobbyId,
                    UserId = Guid.NewGuid(),
                    User = new User()
                    {
                        Id = Guid.NewGuid(),
                        Email = "test@test.com",
                        FirstName = "Jon",
                        LastName = "Snow",
                        Status = false
                    }
                },
                Lobby = new Lobby
                {
                    Id = lobbyId,
                    CreationDate = DateTime.Now,
                    Name = "This is a test lobby",
                    HasPassword = false,
                    Password = "",
                }
            };

            List<Chat> chats = new List<Chat>() { chat };


            Mock<IPaginate<Chat>> paginateMock = new();
            paginateMock.Setup(p => p.Items).Returns(chats);
            paginateMock.Setup(p => p.Index).Returns(0);
            paginateMock.Setup(p => p.Size).Returns(10);
            paginateMock.Setup(p => p.Count).Returns(1);
            paginateMock.Setup(p => p.Pages).Returns(1);
            paginateMock.Setup(p => p.HasPrevious).Returns(false);
            paginateMock.Setup(p => p.HasNext).Returns(false);

            var result = _mapper.Map<ChatListModel>(paginateMock.Object);

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.Count.ShouldBe(1);
            result.Items[0].Message.ShouldBe("This is a test message");
            result.Count.ShouldBe(1);
            result.Index.ShouldBe(0);
            result.Size.ShouldBe(10);
            result.Pages.ShouldBe(1);
            result.HasPrevious.ShouldBeFalse();
            result.HasNext.ShouldBeFalse();
        }
    }
}
