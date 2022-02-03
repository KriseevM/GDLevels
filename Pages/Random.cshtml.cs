using System;
using System.Linq;
using GDLevels.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDLevels.Pages
{
    public class RandomModel : PageModel
    {
        private GDLevelsDataContext _levelsContext;

        public int LevelsCount
        {
            get => _levelsContext.Levels.Count();
        }

        public int SelectedID { get; private set; } = 0;
        public bool IsKeyValid { get; private set; } = false;

        public RandomModel(GDLevelsDataContext levelsContext)
        {
            _levelsContext = levelsContext;
        }

        public void OnGet()
        {
        }

        public void OnPostRandomize()
        {
            Random rand = new Random();
            SelectedID = _levelsContext.Levels.ToList()[rand.Next(0, _levelsContext.Levels.Count())].LevelId;
        }
    }
}