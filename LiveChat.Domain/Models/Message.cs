using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Message
    {
        public User Sender { get; }
        public User Recipient { get; }
        public string Content { get; }

        public Message(User sender, User recipient, string content)
        {
            Sender = sender;
            Recipient = recipient;
            Content = content;
        }
    }
}
