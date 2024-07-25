using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Responses
{
    public class MessageResponse
    {
        public UserResponse Sender { get; set; }

        public UserResponse Recipient { get; set; }

        public string Content { get; set; }
    }
}
