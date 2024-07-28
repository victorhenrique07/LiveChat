using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class GuildMember
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int GuildId { get; set; }
        public virtual Guild Guild { get; set; }
    }
}
