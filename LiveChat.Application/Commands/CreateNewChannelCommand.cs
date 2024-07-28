using LiveChat.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class CreateNewChannelCommand : IRequest<ChannelResponse>
    {
        public string Name { get; set; }
        public int GuildId { get; set; }
    }
}
