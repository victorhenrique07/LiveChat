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
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public string Content { get; set; }
    }
}
