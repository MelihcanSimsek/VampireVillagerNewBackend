using Application.Features.Votes.Commands.CreateVote;
using Application.Features.Votes.Commands.DeleteVote;
using Application.Features.Votes.Commands.UpdateVote;
using Application.Features.Votes.Dtos;
using Application.Features.Votes.Models;
using Application.Features.Votes.Queries.GetDayStateVoteList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateVoteCommand createVoteCommand)
        {
            CreatedVoteDto result = await Mediator.Send(createVoteCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteVoteCommand deleteVoteCommand)
        {
            DeletedVoteDto result = await Mediator.Send(deleteVoteCommand);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateVoteCommand updateVoteCommand)
        {
            UpdatedVoteDto result = await Mediator.Send(updateVoteCommand);
            return Ok(result);
        }

        [HttpGet("GetDayStateVoteList")]
        public async Task<IActionResult> GetDayStateVoteList([FromQuery] int day, [FromQuery] Guid gameSettingId, [FromQuery] PageRequest pageRequest)
        {
            GetDayStateVoteListQuery query = new() { Day = day, PageRequest = pageRequest, GameSettingId = gameSettingId };
            GetDayStateVoteListModel result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
