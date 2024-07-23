using LiveChat.Application.Commands;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using LiveChat.Infraestructure.Repository.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var sender = await userRepository.GetUserByIdAsync(request.SenderId);
            var recipient = await userRepository.GetUserByIdAsync(request.RecipientId);

            if (sender == null || recipient == null)
            {
                throw new Exception("Sender or recipient not found.");
            }

            var message = new Message(sender, recipient, request.Content);
            var key = (Math.Min(request.SenderId, request.RecipientId), Math.Max(request.SenderId, request.RecipientId));

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
