using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace GDLevels.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _config;
        private bool _isAuthorized = false;
        public bool IsAuthorized => _isAuthorized;

        public AdminModel(IConfiguration config)
        {
            _config = config;
        }

        public ActionResult OnGetAsync()
        {
            byte[] isAuthData;
            if (HttpContext.Session.TryGetValue("isAuth", out isAuthData))
            {
                _isAuthorized = isAuthData[0] == 1;
            }
            else
            {
                _isAuthorized = false;
                return RedirectToPage("auth", new {redirectPage = "admin"});
            }

            if (!_isAuthorized)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}