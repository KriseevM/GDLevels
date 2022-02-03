using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GDLevels.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace GDLevels.Services
{
    public class VkOauthCheckerService : IOAuthCheckerService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public VkOauthCheckerService(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;

            _client.BaseAddress = new Uri("https://api.vk.com/method/");
        }

        public async Task<bool> CheckByTokenAsync(string token)
        {
            var requestResult = await _client.GetAsync($"users.get?access_token={token}&v=5.131");
            var requestResultReading = requestResult.Content.ReadAsStringAsync();
            string[] validIds = _config["VK:accepted_ids"].Split(",");
            JsonDocument resultMessageDecoded = JsonDocument.Parse(await requestResultReading);
            if (resultMessageDecoded.RootElement.TryGetProperty("response", out var response))
            {
                var responseEnumerator = response.EnumerateArray();
                responseEnumerator.MoveNext();
                return validIds.Contains(responseEnumerator.Current.GetProperty("id").GetInt32().ToString());
            }

            return false;
        }
    }
}