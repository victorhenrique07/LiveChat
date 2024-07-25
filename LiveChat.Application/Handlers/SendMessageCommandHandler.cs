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
        private readonly Dictionary<(int, int), Queue<Message>> _messageQueues;
        private readonly object _lock;

        private readonly IUserRepository userRepository;

        public SendMessageCommandHandler(Dictionary<(int, int), Queue<Message>> messageQueues, object lockObject, IUserRepository userRepository)
        {
            _messageQueues = messageQueues;
            _lock = lockObject;
            this.userRepository = userRepository;
        }

        public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var sender = await userRepository.GetUserByEmailAsync(request.SenderId);
            var recipient = await userRepository.GetUserByEmailAsync(request.RecipientId);

            if (sender == null || recipient == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Sender or recipient not found.");
            }

            var message = new Message(sender, recipient, request.Content);
            var key = (Math.Min(sender.Id, recipient.Id), Math.Max(sender.Id, recipient.Id));

            lock (_lock)
            {
                if (!_messageQueues.ContainsKey(key))
                {
                    _messageQueues[key] = new Queue<Message>();
                }
                _messageQueues[key].Enqueue(message);
            }

            return new SendMessageResponse { Message = "Message sent successfully" };
        }
    }
}
