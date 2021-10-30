using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiTemplate.Application.Commands;
using ApiTemplate.Application.Queries;
using ApiTemplate.Contract;
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
        public async Task<List<UserDto>> GetAll() => await _mediator.Send(new GetAllUsersQuery());

        [HttpGet("{userId:guid}")]
        public async Task<UserDto> Get(Guid userId) => await _mediator.Send(new GetUserQuery(userId));

        [HttpPost]
        public async Task<Guid> Post(AddUserRequest request) =>
            await _mediator.Send(new AddUserCommand(request.Name, request.Email, request.Role));

        [HttpPut("{userId:guid}")]
        public async Task Put(Guid userId, AddUserRequest request) =>
            await _mediator.Send(new UpdateUserCommand(userId, request.Name, request.Email, request.Role));

        [HttpDelete("{userId:guid}")]
        public async Task Delete(Guid userId) => await _mediator.Send(new DeleteUserCommand(userId));
    }
}