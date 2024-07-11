using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Application.Queries;
using LiveChat.Domain.Repository;
using LiveChat.Infraestructure.Repository.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiveChat.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private const string TokenSecret = "bXlhcHB2ZXJ5c3Ryb25nc2VjcmV0a2V5MTIzNDU2";
        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);

        private readonly IMediator mediator;
        private IConfiguration configuration;

        private readonly IUserRepository userRepository;

        public UserController(IMediator mediator, IConfiguration config, IUserRepository userRepository)
        {
            this.mediator = mediator;
            this.configuration = config;
            this.userRepository = userRepository;
        }

        [HttpGet("all-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await mediator.Send(new GetUserListQuery());

            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var login = await mediator.Send(command);

            return Ok(login);
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
