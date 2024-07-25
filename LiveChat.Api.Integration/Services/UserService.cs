using LiveChat.Api.Integration.Commands;
using LiveChat.Api.Integration.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Api.Integration.Services
{
    public class UserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SignUpService(RegisterUserCommand command)
        {
            var response = await httpClient.PostAsJsonAsync("sign-up", command);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Response: {responseContent}");
            }

            response.EnsureSuccessStatusCode();
        }


        public async Task LoginService(LoginUserCommand command)
        {
            var response = await httpClient.PostAsJsonAsync("login", command);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Response: {responseContent}");
            }

            var statusCode = response.EnsureSuccessStatusCode();
        }
    }
}
