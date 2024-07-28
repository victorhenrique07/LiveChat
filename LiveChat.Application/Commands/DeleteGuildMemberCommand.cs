using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class DeleteGuildMemberCommand : IRequest<IEnumerable<GuildMembersResponse>>
    {
        public int GuildId { get; set; }

        public string UserEmail { get; set; }
    }
}
