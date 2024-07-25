using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class AddFriendCommand : IRequest<UserResponse>
    {
        public int CurrentUserid { get; set; }

        public string? Email { get; set; }

        public int? Id { get; set; }
    }
}
