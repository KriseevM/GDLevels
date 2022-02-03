using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GDLevels.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GDLevels.Services
{
    public class VkOauthPerformerService : IOAuthPerformerService
    {
        private readonly string _oAuthUri;
        private readonly IConfiguration _config;
        private readonly HttpClient _client;

        public VkOauthPerformerService(IConfiguration config, HttpClient client)
        {
            _config = config;
            _client = client;
            _client.BaseAddress = new Uri("https://oauth.vk.com/access_token");
            _oAuthUri =
                $"https://oauth.vk.com/authorize?client_id={_config["VK:client_id"]}&client_secret={_config["VK:client_id"]}&response_type=code&redirect_uri=";
        }


        public string GetOAuthUri(string redirectUri)
        {
            return _oAuthUri + redirectUri;
        }

        public async Task<string> GetTokenByCodeAsync(string code, string redirectUri)
        {
            var tokenRequestResult =
                await _client.GetAsync(
                    $"?client_id={_config["VK:client_id"]}&client_secret={_config["VK:client_secret"]}&redirect_uri={redirectUri}&code={code}");
            var tokenRequestResultReading = tokenRequestResult.Content.ReadAsStringAsync();
            var tokenDocument = JsonDocument.Parse(await tokenRequestResultReading);
            if (tokenDocument.RootElement.TryGetProperty("access_token", out var token))
            {
                return token.GetString();
            }

            if (tokenDocument.RootElement.TryGetProperty("error", out var error))
            {
                throw new Exception(
                    $"Request was finished with error: {tokenDocument.RootElement.GetProperty("error_description").GetString()} (error id: {error.GetString()})");
            }

            throw new Exception("Decoded object does not contain any useful info");
        }
    }
}