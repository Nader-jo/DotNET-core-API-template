using System;
using System.Threading.Tasks;
using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ApiController
    {
        public record AddUserRequest(string Name, string Email, string Role);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(await Mediator.Send(new GetUserQuery(userId)));
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(await Mediator.Send(new AddUserCommand(request.Name, request.Email, request.Role)));
        }

        [HttpPut("{userId:guid}")]
        public async Task<IActionResult> Put(Guid userId, AddUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(await Mediator.Send(new UpdateUserCommand(userId, request.Name, request.Email, request.Role)));
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            return Ok(await Mediator.Send(new DeleteUserCommand(userId)));
        }
    }
}