﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class DeleteUserCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
