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
    public class LoginUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
