using Application.Features.Lobbies.Commands.CreateLobby;
using Application.Features.Lobbies.Commands.DeleteLobby;
using Application.Features.Lobbies.Dtos;
using Application.Features.Lobbies.Models;
using Application.Features.Lobbies.Queries.GetListLobby;
using Application.Features.Lobbies.Queries.GetListPrivateLobby;
using Application.Features.Lobbies.Queries.GetListPublicLobby;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbiesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateLobbyCommand createLobbyCommand)
        {
            CreatedLobbyDto result = await Mediator.Send(createLobbyCommand);
            return Created("", result);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteLobbyCommand deleteLobbyCommand)
        {
            DeletedLobbyDto result = await Mediator.Send(deleteLobbyCommand);
            return Ok(result);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLobbyQuery getListLobbyQuery = new() { PageRequest = pageRequest };
            LobbyListModel result = await Mediator.Send(getListLobbyQuery);
            return Ok(result);
        }

        [HttpGet("GetListPrivateLobby")]
        public async Task<IActionResult> GetListPrivateLobby([FromQuery] PageRequest pageRequest)
        {
            GetListPrivateLobbyQuery getListPrivateLobbyQuery = new() { PageRequest = pageRequest };
            LobbyListModel result = await Mediator.Send(getListPrivateLobbyQuery);
            return Ok(result);
        }


        [HttpGet("GetListPublicLobby")]
        public async Task<IActionResult> GetListPublicLobby([FromQuery] PageRequest pageRequest)
        {
            GetListPublicLobbyQuery getListPublicLobbyQuery = new() { PageRequest = pageRequest };
            LobbyListModel result = await Mediator.Send(getListPublicLobbyQuery);
            return Ok(result);
        }



    }
}
