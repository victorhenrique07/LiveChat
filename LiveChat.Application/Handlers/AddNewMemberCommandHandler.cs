using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class AddNewMemberCommandHandler : IRequestHandler<AddGuildMemberCommand, IEnumerable<GuildMembersResponse>>
    {
        private readonly IUserRepository userRepository;
        private readonly IGuildRepository guildRepository;

        public AddNewMemberCommandHandler(IUserRepository userRepository, IGuildRepository guildRepository)
        {
            this.userRepository = userRepository;
            this.guildRepository = guildRepository;
        }

        public async Task<IEnumerable<GuildMembersResponse>> Handle(AddGuildMemberCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetUserByEmailAsync(command.UserEmail);
            Guild guild = await guildRepository.GetAvailableGuild(command.GuildId);

            IReadOnlyCollection<GuildMember> updatedGuildMembers = await guildRepository.AddMemberAsync(user, guild);

            IEnumerable<GuildMembersResponse> guildMembers = updatedGuildMembers.Select(x => new GuildMembersResponse
            {
                Name = x.User.Name,
                Email = x.User.Email
            });

            return guildMembers;
        }
    }
}
