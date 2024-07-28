using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Channel
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        [Required]
        public int GuildId { get; set; }
        public Guild Guild { get; set; }

        public IReadOnlyCollection<Message>? Messages { get; set; } = new List<Message>();
    }
}
