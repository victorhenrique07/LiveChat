using LiveChat.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Queries
{
    public class GetUserFriendsQuery : IRequest<IEnumerable<UserResponse>>
    {
        public int CurrentUserId { get; set; }
    }
}
