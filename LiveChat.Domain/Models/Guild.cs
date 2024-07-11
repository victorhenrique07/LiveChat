using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Guild
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<Channel> Channels { get; set; }
    }
}
