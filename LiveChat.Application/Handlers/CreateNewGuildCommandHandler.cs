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
    public class CreateNewGuildCommandHandler : IRequestHandler<CreateNewGuildCommand, GuildResponse>
    {
        private readonly IGuildRepository guildRepository;
        private readonly IUserRepository userRepository;

        public CreateNewGuildCommandHandler(IGuildRepository guildRepository, IUserRepository userRepository)
        {
            this.guildRepository = guildRepository;
            this.userRepository = userRepository;
        }

        public async Task<GuildResponse> Handle(CreateNewGuildCommand command, CancellationToken cancellationToken)
        {
            // Obtenha o proprietário do guild
            User guildOwner = await userRepository.GetUserByIdAsync(command.OwnerId);
            if (guildOwner == null)
            {
                throw new Exception("Guild owner not found.");
            }

            // Inicialize a lista de membros do guild
            List<GuildMember> guildMembers = new List<GuildMember>();

            // Adicione cada membro à lista de membros do guild
            foreach (int memberId in command.Members)
            {
                User member = await userRepository.GetUserByIdAsync(memberId);
                if (member == null)
                {
                    throw new Exception($"User with ID {memberId} not found.");
                }

                guildMembers.Add(new GuildMember { UserId = member.Id });
            }

            // Crie o novo guild com o proprietário e os membros
            Guild guildDetails = new Guild
            {
                Name = command.Name,
                Members = guildMembers,
                Owner = guildOwner
            };

            // Salve o novo guild no repositório
            await guildRepository.CreateNewGuild(guildDetails);

            // Prepare a resposta com os detalhes do guild criado
            GuildResponse guildResponse = new GuildResponse
            {
                Name = guildDetails.Name,
                Members = guildMembers.Select(gm => new UserResponse
                {
                    Email = gm.User.Email,
                    Name = gm.User.Name
                }).ToList(),
                Owner = new UserResponse
                {
                    Email = guildOwner.Email,
                    Name = guildOwner.Name
                }
            };

            return guildResponse;
        }

    }
}
