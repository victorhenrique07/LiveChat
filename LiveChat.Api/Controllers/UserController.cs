using LiveChat.Application;
using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Application.Queries;
using LiveChat.Domain.Models;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public UserController(IMediator mediator, IConfiguration config)
        {
            this.mediator = mediator;
            this.configuration = config;
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

            if (login == null)
            {
                return Unauthorized("Login failed");
            }

            return Ok(login);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> AddUserAsync([FromBody] RegisterUserCommand command)
        {
            var studentDetail = await mediator.Send(command);

            return Ok(studentDetail);
        }

        [Authorize]
        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            command.SenderId = int.Parse(userId);

            var handler = await mediator.Send(command);

            return Ok(handler);
        }

        [Authorize]
        [HttpGet("get-messages")]
        public async Task<IActionResult> GetMessages()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var messages = await mediator.Send(new GetMessagesQuery() { UserId = int.Parse(userId) });

            return Ok(messages);
        }
    }
}
