using Application.Features.Claims.Commands.CreateClaim;
using Application.Features.Claims.Commands.DeleteClaim;
using Application.Features.Claims.Commands.UpdateClaim;
using Application.Features.Claims.Dtos;
using Application.Features.Claims.Models;
using Application.Features.Claims.Queries.GetListOperationClaim;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateClaimCommand createClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createClaimCommand);
            return Created("", result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            DeleteClaimCommand deleteClaimCommand = new() { Id = id };
            DeletedOperationClaimDto result = await Mediator.Send(deleteClaimCommand);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateClaimCommand updateClaimCommand)
        {
            UpdatedOperationClaimDto result = await Mediator.Send(updateClaimCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }


    }
}
