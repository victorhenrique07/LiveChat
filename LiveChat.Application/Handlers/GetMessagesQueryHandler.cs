using LiveChat.Application.Queries;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IEnumerable<Message>>
    {
        private readonly Dictionary<(int, int), Queue<Message>> _messageQueues;
        private readonly object _lock;

        public GetMessagesQueryHandler(Dictionary<(int, int), Queue<Message>> messageQueues, object lockObject)
        {
            _messageQueues = messageQueues;
            _lock = lockObject;
        }

        public Task<IEnumerable<Message>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = new List<Message>();

            lock (_lock)
            {
                foreach (var key in _messageQueues.Keys)
                {
                    if (key.Item1 == request.UserId || key.Item2 == request.UserId)
                    {
                        messages.AddRange(_messageQueues[key]);
                    }
                }
            }

            return Task.FromResult(messages.AsEnumerable());
        }
    }
}
