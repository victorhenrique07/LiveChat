using LiveChat.Api.Integration.Commands;
using LiveChat.Api.Integration.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Services
{
    public class MessageService
    {
        private readonly HttpClient httpClient;

        public MessageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SendMessageAsync(SendMessageCommand command)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("send-message", command);

                if (!response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(command.SenderId);
                    Console.WriteLine($"Response Status Code: {response.StatusCode}");
                    Console.WriteLine($"Response Body: {responseBody}");
                }
                else
                {
                    Console.WriteLine("Message sent successfully");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error while trying to send message: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public async Task<List<MessageResponse>> GetMessagesAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("get-messages");
                response.EnsureSuccessStatusCode();

                var messages = await response.Content.ReadFromJsonAsync<List<MessageResponse>>();
                return messages;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error while trying to get messages: {ex.Message}");
                return new List<MessageResponse>();
            }
        }
    }
}
