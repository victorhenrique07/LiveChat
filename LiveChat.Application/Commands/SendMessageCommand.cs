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
    public class SendMessageCommand : IRequest<SendMessageResponse>
    {
        public string SenderId { get; set; } = "";
        public int GuildId { get; set; }
        public int ChannelId { get; set; }
        public string Content { get; set; }
    }
}
