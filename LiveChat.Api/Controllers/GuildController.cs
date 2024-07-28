using LiveChat.Application.Commands;
using LiveChat.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiveChat.Api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class GuildController : ControllerBase
    {
        private readonly IMediator mediator;
        private IConfiguration configuration;

        public GuildController(IMediator mediator, IConfiguration configuration)
        {
            this.mediator = mediator;
            this.configuration = configuration;
        }

        [Authorize]
        [HttpGet("guilds/{guildId}")]
        public async Task<IActionResult> GetAvailableGuild(int guildId)
        {
            var query = new GetAvailableGuildQuery() { Id = guildId };

            var handler = await mediator.Send(query);

            if (handler == null)
                return NotFound("Guild not found.");

            return Ok(handler);
        }

        [Authorize]
        [HttpPost("guilds")]
        public async Task<IActionResult> CreateNewGuild([FromBody] CreateNewGuildCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request.");

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            command.OwnerId = int.Parse(userId);

            var handler = await mediator.Send(command);

            return Ok(handler);
        }

        [Authorize]
        [HttpPost("guilds/{guildId}/add-user/{userEmail}")]
        public async Task<IActionResult> AddNewMember(int guildId, string userEmail)
        {
            if (guildId == null || userEmail == null)
                return BadRequest("Invalid Request.");

            var handler = await mediator.Send(new AddGuildMemberCommand() { GuildId = guildId, UserEmail = userEmail });

            return Ok(handler);
        }

        [Authorize]
        [HttpDelete("guilds/{guildId}/members/{userEmail}")]
        public async Task<IActionResult> DeleteGuildMember(int guildId, string userEmail)
        {
            if (guildId <= 0 || string.IsNullOrEmpty(userEmail))
                return BadRequest("Invalid Request.");

            var handler = await mediator.Send(new DeleteGuildMemberCommand() { GuildId = guildId, UserEmail = userEmail });

            return Ok(handler);
        }

        [Authorize]
        [HttpPost("guilds/{guildId}/channels/{channelName}")]
        public async Task<IActionResult> CreateNewChannel(int guildId, string channelName)
        {
            if (guildId <= 0 || string.IsNullOrEmpty(channelName))
                return BadRequest("Invalid Request.");

            var handler = await mediator.Send(new CreateNewChannelCommand() { GuildId = guildId, Name = channelName });

            return Ok(handler);
        }

        [Authorize]
        [HttpGet("guilds/{guildId}/channels/{channelId}/messages")]
        public async Task<IActionResult> GetMessages(int guildId, int channelId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var messages = await mediator.Send(new GetMessagesQuery() { ChannelId = channelId, GuildId = guildId });

            return Ok(messages);
        }
    }
}
