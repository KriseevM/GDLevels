using System;
using System.Linq;
using GDLevels.Data.Adapters.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDLevels.Pages
{
    public class RandomModel : PageModel
    {
        private ILevelsDataAdapter _levelsData;

        public int LevelsCount
        {
            get => _levelsData.LevelsCount;
        }

        public int SelectedID { get; private set; } = 0;

        public RandomModel(ILevelsDataAdapter levelsData)
        {
            _levelsData = levelsData;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostRandomize()
        {
            Random rand = new Random();
            if (_levelsData.LevelsCount > 0)
            {
                SelectedID = _levelsData.Context.Levels.ToList()[rand.Next(0, _levelsData.Context.Levels.Count())].LevelId;
                return Page();
            }
            return RedirectToPage("Random");
        }
    }
}