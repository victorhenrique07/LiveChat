using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Commands
{
    public class CreateNewGuildCommand : IRequest<GuildResponse>
    {
        public string Name { get; set; }

        public IReadOnlyCollection<int>? Members { get; set; }

        public int OwnerId { get; set; } = 0;
    }
}
