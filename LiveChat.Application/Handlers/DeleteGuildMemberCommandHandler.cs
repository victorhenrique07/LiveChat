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
    public class DeleteGuildMemberCommandHandler : IRequestHandler<DeleteGuildMemberCommand, IEnumerable<GuildMembersResponse>>
    {
        private readonly IUserRepository userRepository;
        private readonly IGuildRepository guildRepository;

        public DeleteGuildMemberCommandHandler(IUserRepository userRepository, IGuildRepository guildRepository)
        {
            this.userRepository = userRepository;
            this.guildRepository = guildRepository;
        }

        public async Task<IEnumerable<GuildMembersResponse>> Handle(DeleteGuildMemberCommand command, CancellationToken cancellationToken)
        {
            User user = await userRepository.GetUserByEmailAsync(command.UserEmail);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            Guild guild = await guildRepository.GetAvailableGuild(command.GuildId);

            if (guild == null)
            {
                throw new Exception("Guild not found.");
            }

            IReadOnlyCollection<GuildMember> updatedGuildMembers = await guildRepository.DeleteUserAsync(user, guild);

            IEnumerable<GuildMembersResponse> guildMembers = updatedGuildMembers.Select(x => new GuildMembersResponse
            {
                Name = x.User.Name,
                Email = x.User.Email
            });

            return guildMembers;
        }
    }
}
