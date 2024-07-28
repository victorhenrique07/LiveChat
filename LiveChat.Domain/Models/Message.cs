using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Message
    {
        public int Id { get; private set; }

        public int SenderId { get; set; }

        [Required]
        public User Sender { get; set; }

        [Required]
        public string Content { get; set; }

        public int ChannelId { get; set; }

        public Channel Channel { get; set; } 

        public Message(User sender, string content)
        {
            Sender = sender;
            Content = content;
        }

        public Message() { }
    }
}
