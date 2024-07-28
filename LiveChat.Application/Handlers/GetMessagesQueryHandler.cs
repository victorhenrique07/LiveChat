using LiveChat.Application.Models;
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
    public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, ChannelResponse>
    {
        private readonly IGuildRepository guildRepository;
        private readonly Dictionary<(int, int), Queue<Message>> _messageQueues;
        private readonly object _lock;

        public GetMessagesQueryHandler(Dictionary<(int, int), Queue<Message>> messageQueues, object lockObject, IGuildRepository guildRepository)
        {
            _messageQueues = messageQueues;
            _lock = lockObject;
            this.guildRepository = guildRepository;
        }

        public Task<ChannelResponse> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            string channelName = guildRepository.GetAvailableChannel(request.GuildId, request.ChannelId).Result.Name;

            var messages = new List<Message>();

            lock (_lock)
            {
                foreach (var key in _messageQueues.Keys)
                {
                    if (key.Item1 == request.ChannelId || key.Item2 == request.ChannelId)
                    {
                        messages.AddRange(_messageQueues[key]);
                    }
                }
            }

            ChannelResponse channelResponse = new ChannelResponse
            {
                Name = channelName,
                Messages = messages.Select(x => new MessageResponse
                {
                    SenderName = x.Sender.Name,
                    Content = x.Content,
                }).ToList()
            };

            return Task.FromResult(channelResponse);
        }
    }
}
