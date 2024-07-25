using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Commands
{
    public class SendMessageCommand
    {
        public string SenderId { get; set; } = "";
        public string RecipientId { get; set; }
        public string Content { get; set; }
    }
}
