using Application.Features.Lobbies.Commands.CreateLobby;
using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Profiles;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VampireVillagers.Application.UnitTests.Features.Lobbies.Profiles
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
        public void Mapper_Should_Map_CreateLobbyCommand_To_Lobby()
        {
            var command = new CreateLobbyCommand()
            {
                Name = "This is a test lobby",
                HasPassword = true,
                Password = "secret-password",
                CreationDate = DateTime.Now
            };

            var result = _mapper.Map<Lobby>(command);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(command.Name);
            result.Password.ShouldBe(command.Password);
            result.HasPassword.ShouldBe(command.HasPassword);
            result.CreationDate.ShouldBe(command.CreationDate);
        }

        [Fact]
        public void Mapper_Should_Map_Lobby_To_CreatedLobbyDto()
        {
            var lobby = new Lobby()
            {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "secret-password",
                Name = "This is a test lobby",
                Id = Guid.NewGuid(),
            };

            var result = _mapper.Map<CreatedLobbyDto>(lobby);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(lobby.Name);
            result.HasPassword.ShouldBe(lobby.HasPassword);
            result.Password.ShouldBe(lobby.Password);
            result.CreationDate.ShouldBe(lobby.CreationDate);
        }

        [Fact]
        public void Mapper_Should_Map_Lobby_To_LobbyListDto()
        {
            var lobby = new Lobby()
            {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "secret-password",
                Name = "This is a test lobby",
                Id = Guid.NewGuid(),
            };

            var result = _mapper.Map<LobbyListDto>(lobby);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(lobby.Name);
            result.CreationDate.ShouldBe(lobby.CreationDate);
            result.HasPassword.ShouldBe(lobby.HasPassword);
            result.Id.ShouldBe(lobby.Id);
        }

        [Fact]
        public void Mapper_Should_Map_Lobby_To_DeletedLobbyDto()
        {
            var lobby = new Lobby()
            {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "secret-password",
                Name = "This is a test lobby",
                Id = Guid.NewGuid(),
            };

            var result = _mapper.Map<DeletedLobbyDto>(lobby);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(lobby.Name);
            result.CreationDate.ShouldBe(lobby.CreationDate);
        }

        [Fact]
        public void Mapper_Should_Map_IPaginateLobby_To_LobbyListModel()
        {
            var lobbies = new List<Lobby>
            {
                new Lobby
                {
                CreationDate = DateTime.Now,
                HasPassword = true,
                Password = "secret-password",
                Name = "This is a private test lobby",
                Id = Guid.NewGuid(),
                },
                new Lobby
                {
                CreationDate = DateTime.Now,
                HasPassword = false,
                Password = "",
                Name = "This is a public test lobby",
                Id = Guid.NewGuid(),
                }

            };

            Mock<IPaginate<Lobby>> paginateMock = new();
            paginateMock.Setup(p => p.Items).Returns(lobbies);
            paginateMock.Setup(p => p.Index).Returns(0);
            paginateMock.Setup(p => p.Size).Returns(10);
            paginateMock.Setup(p => p.Count).Returns(2);
            paginateMock.Setup(p => p.Pages).Returns(1);
            paginateMock.Setup(p => p.HasPrevious).Returns(false);
            paginateMock.Setup(p => p.HasNext).Returns(false);

            var result = _mapper.Map<LobbyListModel>(paginateMock.Object);

            result.ShouldNotBeNull();
            result.Items.ShouldNotBeNull();
            result.Items.Count.ShouldBe(2);
            result.Items[0].Name.ShouldBe("This is a private test lobby");
            result.Count.ShouldBe(2);
            result.Index.ShouldBe(0);
            result.Size.ShouldBe(10);
            result.Pages.ShouldBe(1);
            result.HasPrevious.ShouldBeFalse();
            result.HasNext.ShouldBeFalse();
        }
    }
}
