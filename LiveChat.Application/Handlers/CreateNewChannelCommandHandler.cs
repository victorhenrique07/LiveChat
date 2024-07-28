using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class CreateNewChannelCommandHandler : IRequestHandler<CreateNewChannelCommand, ChannelResponse>
    {
        private readonly IGuildRepository guildRepository;

        public CreateNewChannelCommandHandler(IGuildRepository guildRepository)
        {
            this.guildRepository = guildRepository;
        }

        public async Task<ChannelResponse> Handle(CreateNewChannelCommand command, CancellationToken cancellationToken)
        {
            Guild guild = await guildRepository.GetAvailableGuild(command.GuildId);

            Channel newChannel = new Channel() { Name = command.Name, Guild = guild, GuildId = guild.Id };

            await guildRepository.CreateNewChannel(newChannel);

            return new ChannelResponse()
            {
                Name = newChannel.Name,
                Messages = new List<MessageResponse>()
            };
        }
    }
}
