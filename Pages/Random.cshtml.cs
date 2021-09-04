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
        // �������� ���� ���������� ���������� ��������, ��������� ���� ���� ������������ ��� �������� �������
        private string _validKey = "12345678";
        private GDLevelsDataContext _levelsContext;
        public int SelectedID { get; private set; } = 0;
        public bool IsKeyValid { get; private set; } = false;
        public RandomModel(GDLevelsDataContext levelsContext)
        {
            _levelsContext = levelsContext;
        }
        public void OnGet(string key)
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
            OnGet(key);
            Random rand = new Random();
            SelectedID = _levelsContext.Levels.ToList()[rand.Next(0, _levelsContext.Levels.Count())].LevelID;            
        }

        public void OnPostDeleteLevel(int SelectedID)
        {
        }
    }
}
