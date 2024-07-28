using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Models
{
    public class ChannelResponse
    {
        public string Name { get; set; }

        public IReadOnlyCollection<MessageResponse> Messages { get; set; }
    }
}
