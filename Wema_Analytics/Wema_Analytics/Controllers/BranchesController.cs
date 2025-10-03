using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wema_Analytics.Commands;

namespace Wema_Analytics.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BranchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchesController(IMediator mediator) => _mediator = mediator;

        [HttpPost("AddBranches")]
        public async Task<IActionResult> Create([FromBody] CreateBranch command)
        {
            try
            {
                var branch = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateBranches/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranch command)
        {
            if (id != command.Id) return BadRequest();
            var branch = await _mediator.Send(command);
            return branch is null ? NotFound() : Ok(branch);
        }

        [HttpPatch("DeactivateBranches/{id}")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var result = await _mediator.Send(new DeactivateBranch(id));
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("DeleteBranches/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteBranch(id));
            return result ? NoContent() : NotFound();
        }

        [HttpGet("GetBranches/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var branch = await _mediator.Send(new GetBranchById(id));
            return branch is null ? NotFound() : Ok(branch);
        }


        [HttpGet("SearchBranches")]
        public async Task<IActionResult> Search([FromQuery] string? city, [FromQuery] string? region)
        {
            var branches = await _mediator.Send(new SearchBranches(city, region));
            return Ok(branches);
        }
    }
}
