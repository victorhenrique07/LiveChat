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

        public SendMessageCommandHandler(Dictionary<(int, int), Queue<Message>> messageQueues, object _lock, IUserRepository userRepository)
        {
            this.messageQueues = messageQueues;
            this._lock = _lock;
            this.userRepository = userRepository;
        }

        public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            User sender = await userRepository.GetUserByEmailAsync(request.SenderId);
            User recipient = await userRepository.GetUserByEmailAsync(request.RecipientId);

            if (sender == null || recipient == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Sender or recipient not found.");
            }

            Message message = new Message(sender, recipient, request.Content);
            var key = (Math.Min(sender.Id, recipient.Id), Math.Max(sender.Id, recipient.Id));

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
