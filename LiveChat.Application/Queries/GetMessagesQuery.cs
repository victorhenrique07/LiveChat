using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Queries
{
    public class GetMessagesQuery : IRequest<ChannelResponse>
    {
        public int ChannelId { get; set; }
        public int GuildId { get; set; }
    }
}
