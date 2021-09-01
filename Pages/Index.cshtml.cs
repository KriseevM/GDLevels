using GDLevels.Data;
using GDLevels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDLevels.Pages
{
    public class IndexModel : PageModel
    {
        public int PageNumber { get; set; } = 1;
        public int LastPageNumber { get; set; } = 1;
        public const int LevelsPerPage = 15;
        public string StatusMessage { get; set; } = "";
        public UploadStatus UploadStatus { get; private set; } = UploadStatus.NoRequest;
        private readonly ILogger<IndexModel> _logger;
        public GDLevelsDataContext _levelsContext { get; private set; }
        public IndexModel(ILogger<IndexModel> logger, GDLevelsDataContext levelsContext)
        {
            _logger = logger;
            _levelsContext = levelsContext;
        }
        public async Task<IActionResult> OnPostAsync(int levelid)
        {
            OnGet(1);
            if (levelid < 10000000 || levelid > 99999999)
            {
                UploadStatus = UploadStatus.Fail;
                StatusMessage = "Этого не должно было произойти, но отправленный ID некорректен";
                return Page();
            }
            if (_levelsContext.Levels.Any(p => (p.LevelID == levelid)))
            {
                UploadStatus = UploadStatus.Fail;
                StatusMessage = "Уровень уже есть в базе данных";
                return Page();
            }
            _levelsContext.Levels.Add(new Models.Level() { LevelID = levelid, RequestTime = (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds });

            var addedRows = await _levelsContext.SaveChangesAsync();
            if (addedRows == 1)
            {
                UploadStatus = UploadStatus.Success;
                StatusMessage = "Уровень добавлен в базу данных";
                return Page();
            }
            else
            {
                UploadStatus = UploadStatus.Fail;
                StatusMessage = $"Уровень почему-то не добавлен :(";
                return Page();
            }

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
        public IEnumerable<Level> GetLevelsOnPage()
        {
            return _levelsContext.Levels.OrderBy(p => p.RequestTime).Reverse().Skip((PageNumber - 1) * IndexModel.LevelsPerPage).Take(IndexModel.LevelsPerPage);
        }
    }
    public enum UploadStatus
    {
        NoRequest = 0,
        Success = 1,
        Fail = 2
    }
}
