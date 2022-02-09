using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using GDLevels.Services.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;

namespace GDLevels.Pages
{
    public class Auth : PageModel
    {
        private readonly IConfiguration _config;
        private readonly IOAuthCheckerService _oauthChecker;
        private readonly IOAuthPerformerService _oauthPerformer;

        public Auth(IConfiguration config, IOAuthCheckerService oauthChecker, IOAuthPerformerService oauthPerformer)
        {
            _oauthChecker = oauthChecker;
            _oauthPerformer = oauthPerformer;
            _config = config;
        }

        public async Task<ActionResult> OnGetAsync([FromQuery] string code, [FromQuery] string redirectPage)
        {
            if (redirectPage == null)
                return RedirectToPage("Index");
            if (code == null)
            {
                string uri = Request.GetDisplayUrl();
                HttpContext.Session.Set("RedirectURI", Encoding.UTF8.GetBytes(uri));
                return Redirect(_oauthPerformer.GetOAuthUri(uri));
            }

            byte[] storedUri;
            if (HttpContext.Session.TryGetValue("RedirectURI", out storedUri))
            {
                string token = await _oauthPerformer.GetTokenByCodeAsync(code, Encoding.UTF8.GetString(storedUri));
                bool checkResult = await _oauthChecker.CheckByTokenAsync(token);
                if (checkResult)
                {
                    HttpContext.Session.Set("isAuth", new byte[] {1});
                    return Redirect(redirectPage);
                }

                HttpContext.Session.Set("isAuth", new byte[] {0});
            }

            return RedirectToPage("Index");
        }
    }
}