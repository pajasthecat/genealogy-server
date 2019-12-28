using System;
using System.Threading.Tasks;
using Geneology.Api.Commands;
using Geneology.Api.Models.Contracts;
using Geneology.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Geneology.Api.Controllers
{
    [ApiController]
    [Route("api/v1/familymembers")]
    [Authorize]
    public class FamilyMembersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FamilyMembersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFamilyMemberByIdAsync(string id)
        {
            var query = new GetFamilyMemberByIdQuery(Guid.Parse(id));
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetFamilyMembersAsync()
        {
            var query = new GetFamilyMembersQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddFamilyMember([FromBody] AddFamilyMemberRequest request)
        {
            var command = new AddFamilyMemberCommand(request.Name, request.BirthDate, request.DeathDate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}