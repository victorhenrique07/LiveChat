using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Application.Queries;
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
    public class GetAvailableGuildCommandHandler : IRequestHandler<GetAvailableGuildQuery, GuildResponse>
    {
        public readonly IGuildRepository guildRepository;
        public readonly IUserRepository userRepository;

        public GetAvailableGuildCommandHandler(IGuildRepository guildRepository, IUserRepository userRepository)
        {
            this.guildRepository = guildRepository;
            this.userRepository = userRepository;
        }

        public async Task<GuildResponse> Handle(GetAvailableGuildQuery query, CancellationToken cancellationToken)
        {
            Guild guild = await guildRepository.GetAvailableGuild(query.Id);
            User guildOwner = await userRepository.GetUserByIdAsync(guild.OwnerId);

            GuildResponse guildResponse = new GuildResponse
            {
                Name = guild.Name,
                Owner = new UserResponse
                {
                    Email = guildOwner.Email,
                    Name = guildOwner.Name
                },
                Members = guild.Members.Select(x => new UserResponse
                {
                    Email = x.User.Email,
                    Name = x.User.Name
                }).ToList(),
                Channels = guild.Channels.Select(x => new ChannelResponse
                {
                    Name = x.Name,
                    Messages = x.Messages.Select(x => new MessageResponse
                    {
                        SenderName = x.Sender.Name,
                        Content = x.Content,
                    }).ToList()
                }).ToList()
            };

            return guildResponse;
        }
    }
}
