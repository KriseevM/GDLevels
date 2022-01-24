using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GDLevels
{
    public class CheckVkAuthService : ICheckVkAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public CheckVkAuthService(IConfiguration config, HttpClient client)
        {
            _config = config;
            _httpClient = client;
        }
        public async Task<bool> CheckVkAuthByFlowCodeAsync(string code)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"https://oauth.vk.com/access_token?client_id={_config["VK:client_id"]}&client_secret={_config["VK:client_secret"]}&redirect_uri={_config["VK:auth_redir_uri"]}&code={code}");
            if (message.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }
            JObject messageDecoded = JObject.Parse(await message.Content.ReadAsStringAsync());
            if (messageDecoded.ContainsKey("access_token") && messageDecoded.ContainsKey("user_id"))
            {
                string id = messageDecoded["user_id"]!.Value<string>();
                return _config["VK:accepted_ids"].Split(",").Contains(id);
            }
            return false;
        }
    }
}