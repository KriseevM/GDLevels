using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDLevels.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GDLevels.Pages
{
    public class RandomModel : PageModel
    {
        // Значение этой переменной необходимо изменить, поскольку этот ключ используется для удаления уровней
        private string _validKey = "12345678";
        private GDLevelsDataContext _levelsContext;

        public int LevelsCount { get => _levelsContext.Levels.Count(); }

        public int SelectedID { get; private set; } = 0;
        public bool IsKeyValid { get; private set; } = false;
        public RandomModel(GDLevelsDataContext levelsContext)
        {
            _levelsContext = levelsContext;
        }

        public void OnGet(string key)
        {
            CheckKey(key);
        }
        public void CheckKey(string key)
        {
            if(key == _validKey)
            {
                IsKeyValid = true;
            }
            else
            {
                IsKeyValid = false;
            }    
        }

        public void OnPostRandomize(string key)
        {
            CheckKey(key);
            Random rand = new Random();
            SelectedID = _levelsContext.Levels.ToList()[rand.Next(0, _levelsContext.Levels.Count())].LevelID;            
        }

        public async Task OnPostDeleteLevel(int selectedID, string key)
        {
            CheckKey(key);
            if(IsKeyValid && selectedID >= 10000000 && selectedID <= 99999999)
            {
                if (_levelsContext.Levels.Any(p => p.LevelID == selectedID))
                {
                    _levelsContext.Levels.Remove(_levelsContext.Levels.First(p=>p.LevelID == selectedID));
                }
            }
            int result = await _levelsContext.SaveChangesAsync();
            return;
        }
    }
}
