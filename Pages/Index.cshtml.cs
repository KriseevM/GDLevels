using GDLevels.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using GDLevels.Data.Adapters.Interfaces;
using GDLevels.Misc;

namespace GDLevels.Pages
{
    public class IndexModel : PageModel
    {
        public int PageNumber { get; set; } = 1;
        public int LastPageNumber { get; set; } = 1;
        public const int LevelsPerPage = 15;
        public StatusMessage UploadStatusMessage = new StatusMessage("", false);
        private readonly ILogger<IndexModel> _logger;
        private GDLevelsDataContext _levelsContext;
        private readonly ILevelsDataAdapter _levelsAdapter;
        public ILevelsDataAdapter LevelsAdapter => _levelsAdapter;

        public IndexModel(ILogger<IndexModel> logger, GDLevelsDataContext levelsContext,
            ILevelsDataAdapter levelsAdapter)
        {
            _logger = logger;
            _levelsContext = levelsContext;
            _levelsAdapter = levelsAdapter;
        }

        public async Task<IActionResult> OnPostAsync(int levelid)
        {
            OnGet(1);
            if (levelid < 10000000 || levelid > 99999999)
            {
                UploadStatusMessage =
                    new StatusMessage("Этого не должно было произойти, но отправленный ID некорректен", false);
                return Page();
            }

            UploadStatusMessage = await _levelsAdapter.AddLevelAsync(levelid);
            return Page();
        }

        public IActionResult OnGet(int pageNumber)
        {
            LastPageNumber = _levelsContext.Levels.Count() / LevelsPerPage + 1;
            if (pageNumber > LastPageNumber)
            {
                return Redirect("~/" + LastPageNumber.ToString());
            }
            else if (pageNumber < 1)
            {
                PageNumber = 1;
                return Page();
            }
            else
            {
                PageNumber = pageNumber;
                return Page();
            }
        }
    }
}