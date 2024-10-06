using Loymark.Back.Application.DTOs;
using Loymark.Back.Application.Feature.Users.Commands;
using Loymark.Back.Application.Feature.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Loymark.Back.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _mediator.Send(new UserListQuery());
        }

        [HttpGet("byId/{id}")]
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            return await _mediator.Send(new UserByIdQuery(id));
        }

        [HttpPost]
        public async Task<UserDto> CreateUserAsync(UserCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPatch]
        public async Task<UserDto> UpdateUserAsync(UserUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUserByIdAsync(int id)
        {
            await _mediator.Send(new UserDeleteCommand(id));
        }
    }
}
