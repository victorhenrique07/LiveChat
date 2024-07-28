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

        private readonly IMediator mediator;
        private IConfiguration configuration;

        private readonly ILogger<UserController> logger;

        public UserController(IMediator mediator, IConfiguration configuration, ILogger<UserController> logger)
        {
            this.mediator = mediator;
            this.configuration = configuration;
            this.logger = logger;
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
            if (command.Email.IsNullOrEmpty() || command.Password.IsNullOrEmpty())
            {
                return Unauthorized("Login failed");
            }

            var login = await mediator.Send(command);

            if (login.Token.IsNullOrEmpty())
                return Unauthorized("Login failed");

            logger.LogInformation($"User {command.Email} logged with password {command.Password}");

            return Ok(login);
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> AddUserAsync([FromBody] RegisterUserCommand command)
        {
            if (command == null)
            {
                return BadRequest("Invalid data.");
            }

            var studentDetail = await mediator.Send(command);

            return Ok(studentDetail);
        }

        [Authorize]
        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            if (command == null)
                return BadRequest("Invalid command.");

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            command.SenderId = userId;

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

        [Authorize]
        [HttpPost("add-friend")]
        public async Task<IActionResult> AddFriend([FromBody] AddFriendCommand command)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            command.CurrentUserid = int.Parse(userId);

            var friend = await mediator.Send(command);

            return Ok(friend);
        }

        [Authorize]
        [HttpGet("get-friends")]
        public async Task<IActionResult> GetFriends()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var friends = await mediator.Send(new GetUserFriendsQuery() { CurrentUserId = int.Parse(userId) });

            return Ok(friends);
        }
    }
}
