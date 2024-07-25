using Blazored.LocalStorage;

namespace LiveChat.Apresentation.Auth
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public TokenHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("token");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                Console.WriteLine($"Token added to request: {token}");
            }
            else
            {
                Console.WriteLine("No token found.");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
