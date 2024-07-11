using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Domain.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public User User { get; set; }
    }
}
