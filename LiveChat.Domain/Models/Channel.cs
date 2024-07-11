using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public IReadOnlyCollection<Message> Messages { get; set; }
    }
}
