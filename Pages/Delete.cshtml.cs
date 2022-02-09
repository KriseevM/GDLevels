using System.Net;
using System.Threading.Tasks;
using GDLevels.Data.Adapters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDLevels.Pages
{
    public class DeleteModel : PageModel
    {
        private ILevelsDataAdapter _levelsAdapter;
        public DeleteModel(ILevelsDataAdapter levelsDataAdapter)
        {
            _levelsAdapter = levelsDataAdapter;
        }
        public async Task<ActionResult> OnGet([FromQuery] int levelId, [FromQuery] string redirect)
        {
            if (levelId < 10000000 || redirect == null || !HttpContext.Session.TryGetValue("isAuth", out var isAuthorised) || isAuthorised[0] == 0)
                return RedirectToPage("Index");
            await _levelsAdapter.RemoveLevelAsync(levelId);
            return Redirect(redirect);

        }
    }
}