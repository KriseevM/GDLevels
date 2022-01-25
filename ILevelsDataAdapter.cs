using System.Collections.Generic;
using System.Threading.Tasks;
using GDLevels.Models;

namespace GDLevels
{
    public interface ILevelsDataAdapter
    {
        public IEnumerable<Level> GetLevelsOnPage(int page, int levelsPerPage);
        public Task<StatusMessage> AddLevelAsync(int levelId);
    }
}