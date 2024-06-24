using LiveChat.Application.Commands;
using LiveChat.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LiveChat.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;

        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await mediator.Send(new GetUserListQuery());

            return Ok(users);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> AddUserAsync([FromBody] RegisterUserCommand command)
        {
            var studentDetail = await mediator.Send(command);

            return Ok(studentDetail);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentAsync(int id, string name, string email)
        {
            var isStudentDetailUpdated = await mediator.Send(
                new UpdateUserCommand(id, name, email));
            return Ok(isStudentDetailUpdated);
        }

        [HttpDelete]
        public async Task<int> DeleteStudentAsync(int Id)
        {
            return await mediator.Send(new DeleteUserCommand() { Id = Id });
        }
    }
}
