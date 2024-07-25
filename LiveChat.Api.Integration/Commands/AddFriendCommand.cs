using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Commands
{
    public class AddFriendCommand
    {
        public int CurrentUserid { get; set; }

        public string? Email { get; set; }

        public int? Id { get; set; }
    }
}
