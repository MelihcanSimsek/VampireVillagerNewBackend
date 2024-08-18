using Application.Features.Chats.Commands.CreateChat;
using Application.Features.Chats.Commands.DeleteChat;
using Application.Features.Chats.Dtos;
using Application.Features.Chats.Models;
using Application.Features.Chats.Queries.GetListChatByLobbyId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : BaseController
    {

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateChatCommand createChatCommand)
        {
            CreatedChatDto result = await Mediator.Send(createChatCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteChatCommand deleteChatCommand)
        {
            DeletedChatDto result = await Mediator.Send(deleteChatCommand);
            return Ok(result);
        }

        [HttpGet("GetListChatByLobbyId")]
        public async Task<IActionResult> GetListChatByLobbyId([FromQuery] Guid lobbyId)
        {
            GetListChatByLobbyIdQuery getListChatByLobbyIdQuery = new() { LobbyId = lobbyId };
            ChatListModel result = await Mediator.Send(getListChatByLobbyIdQuery);
            return Ok(result);
        }
    }
}
