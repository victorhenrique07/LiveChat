using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Models
{
    public class GuildResponse
    {
        public string Name { get; set; }
        public UserResponse Owner { get; set; }
        public IReadOnlyCollection<UserResponse>? Members { get; set; }
        public IReadOnlyCollection<ChannelResponse>? Channels { get; set; }
    }
}
