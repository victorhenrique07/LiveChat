﻿using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class RegisterUserCommand : IRequest<User>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public RegisterUserCommand(string Name, string Email, string password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = password;
        }
    }
}
