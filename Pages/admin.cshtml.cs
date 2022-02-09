using GDLevels.Data.Adapters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace GDLevels.Pages
{
    public class AdminModel : PageModel
    {
        private readonly IConfiguration _config;
        private bool _isAuthorized = false;
        public int PageNumber = 1;
        public bool IsAuthorized => _isAuthorized;
        public ILevelsDataAdapter LevelsAdapter { get; }

        public AdminModel(IConfiguration config, ILevelsDataAdapter levelsAdapter)
        {
            _config = config;
            LevelsAdapter = levelsAdapter;
        }

        public ActionResult OnGetAsync(int pageNumber)
        {
            byte[] isAuthData;
            if (HttpContext.Session.TryGetValue("isAuth", out isAuthData))
            {
                _isAuthorized = isAuthData[0] == 1;
            }
            else
            {
                _isAuthorized = false;
                return RedirectToPage("auth", new {redirectPage = "/admin/1"});
            }

            if (!_isAuthorized)
            {
                return RedirectToPage("Index");
            }

            if (pageNumber < 1)
                return Redirect("/admin/1");
            PageNumber = pageNumber;
            return Page();
        }
    }
}