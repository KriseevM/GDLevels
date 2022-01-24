using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace GDLevels.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _config;
        private bool _isAuthorized = false;
        private ICheckVkAuthService _vkAuthChecker;
        public bool IsAuthorized => _isAuthorized;
        public AdminModel(IConfiguration config, ICheckVkAuthService vkAuthChecker)
        {
            _config = config;
            _vkAuthChecker = vkAuthChecker;
        }
        public async Task<ActionResult> OnGetAsync([FromQuery]string code)
        {
            if (code == null)
            {
                return Redirect($"https://oauth.vk.com/authorize?client_id={_config["VK:client_id"]}&redirect_uri={_config["VK:auth_redir_uri"]}");
            }
            _isAuthorized = await _vkAuthChecker.CheckVkAuthByFlowCodeAsync(code);
            return Page();
        }
    }
}