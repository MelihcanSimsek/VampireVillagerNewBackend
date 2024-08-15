using Application.Features.Players.Commands.CreatePlayer;
using Application.Features.Players.Commands.DeletePlayer;
using Application.Features.Players.Dtos;
using Application.Features.Players.Models;
using Application.Features.Players.Queries.GetListPlayerByLobbyId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreatePlayerCommand createPlayerCommand)
        {
            CreatedPlayerDto result = await Mediator.Send(createPlayerCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeletePlayerCommand deletePlayerCommand)
        {
            DeletedPlayerDto result = await Mediator.Send(deletePlayerCommand);
            return Ok(result);
        }

        [HttpGet("GetListByLobbyId")]
        public async Task<IActionResult> GetListByLobbyId([FromQuery] Guid id)
        {
            GetListPlayerByLobbyIdQuery query = new() { LobbyId = id };

            PlayerListModel result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
