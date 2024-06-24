﻿using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Queries
{
    public class GetUserListQuery : IRequest<List<User>>
    {
    }
}
