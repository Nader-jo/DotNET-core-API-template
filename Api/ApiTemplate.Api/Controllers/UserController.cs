using System;
using System.Threading.Tasks;
using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        public record AddUserRequest(string Name, string Email, string Role);

        private readonly IMediator _mediator;

        public UserController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            return Ok(await _mediator.Send(new GetUserQuery(userId)));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            return Ok(await _mediator.Send(new AddUserCommand(request.Name, request.Email, request.Role)));
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Put(Guid userId, AddUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            return Ok(await _mediator.Send(new UpdateUserCommand(userId, request.Name, request.Email, request.Role)));
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            return Ok(await _mediator.Send(new DeleteUserCommand(userId)));
        }
    }
}