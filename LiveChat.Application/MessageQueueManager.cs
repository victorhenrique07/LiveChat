using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application
{
    /*
    public class MessageQueueManager
    {
        private readonly Dictionary<int, Queue<Message>> _messageQueues = new Dictionary<int, Queue<Message>>();
        private readonly object _lock = new object();

        public void SendMessage(Message message)
        {
            lock (_lock)
            {
                if (!_messageQueues.ContainsKey(message.Recipient.Id))
                {
                    _messageQueues[message.Recipient.Id] = new Queue<Message>();
                }
                _messageQueues[message.Recipient.Id].Enqueue(message);
            }

            // Iniciar uma thread para processar a fila de mensagens do destinatário
            Task.Run(() => ProcessQueue(message.Recipient.Id));
        }

        public IEnumerable<Message> GetMessages(int userId)
        {
            lock (_lock)
            {
                if (_messageQueues.ContainsKey(userId))
                {
                    return _messageQueues[userId].ToList();
                }
                return Enumerable.Empty<Message>();
            }
        }

        private void ProcessQueue(int userId)
        {
            lock (_lock)
            {
                if (_messageQueues.ContainsKey(userId) && _messageQueues[userId].Any())
                {
                    var message = _messageQueues[userId].Dequeue();

                    Console.WriteLine($"Mensagem entregue: {message.Content}");
                }
            }
        }
    }*/
}
