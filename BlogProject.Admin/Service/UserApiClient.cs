using BlogProject.ViewModel.System.Users;
using Newtonsoft.Json;
using System.Text;

namespace BlogProject.Admin.Service
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> Authencate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var http = new StringContent(json, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            var respone = await client.PostAsync("/api/users/authencate", http);
            var token = await respone.Content.ReadAsStringAsync();
            return token;
        }
    }
}
