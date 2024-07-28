using LiveChat.Application.Commands;
using LiveChat.Application.Exceptions;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using LiveChat.Infraestructure.Repository.Users;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
    {
        private readonly Dictionary<(int, int), Queue<Message>> messageQueues;
        private readonly object _lock;

        private readonly IUserRepository userRepository;
        private readonly IGuildRepository guildRepository;

        public SendMessageCommandHandler(Dictionary<(int, int), Queue<Message>> messageQueues, object _lock, IUserRepository userRepository, IGuildRepository guildRepository)
        {
            this.messageQueues = messageQueues;
            this._lock = _lock;
            this.userRepository = userRepository;
            this.guildRepository = guildRepository;
        }

        public async Task<SendMessageResponse> Handle(SendMessageCommand command, CancellationToken cancellationToken)
        {
            User sender = await userRepository.GetUserByEmailAsync(command.SenderId);
            Channel channel = await guildRepository.GetAvailableChannel(command.GuildId, command.ChannelId);

            if (sender == null || command == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Sender or Channel not found.");
            }

            Message message = new Message(sender, command.Content, channel);
            var key = (Math.Min(sender.Id, command.ChannelId), Math.Max(sender.Id, command.ChannelId));

            lock (_lock)
            {
                if (!messageQueues.ContainsKey(key))
                {
                    messageQueues[key] = new Queue<Message>();
                }
                messageQueues[key].Enqueue(message);
            }

            return new SendMessageResponse { Message = "Message sent successfully" };
        }
    }
}
