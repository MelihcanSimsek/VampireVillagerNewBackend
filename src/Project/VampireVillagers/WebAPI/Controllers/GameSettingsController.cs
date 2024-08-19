using Application.Features.GameSettings.Commands.CreateGameSetting;
using Application.Features.GameSettings.Commands.DeleteGameSetting;
using Application.Features.GameSettings.Dtos;
using Application.Features.GameSettings.Queries.GetGameSettingByLobbyId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSettingsController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateGameSettingCommand createGameSettingCommand)
        {
            CreatedGameSettingDto result = await Mediator.Send(createGameSettingCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteGameSettingCommand deleteGameSettingCommand)
        {
            DeletedGameSettingDto result = await Mediator.Send(deleteGameSettingCommand);
            return Ok(result);
        }

        [HttpGet("GetByLobbyId")]
        public async Task<IActionResult> GetByLobbyId([FromQuery] Guid lobbyId)
        {
            GetGameSettingByLobbyIdQuery getGameSettingByLobbyIdQuery = new() { LobbyId = lobbyId };
            GetGameSettingByLobbyIdDto result = await Mediator.Send(getGameSettingByLobbyIdQuery);
            return Ok(result);
        }
    }
}
