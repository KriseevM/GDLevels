using System.Collections.Generic;
using System.Threading.Tasks;
using GDLevels.Misc;
using GDLevels.Models;

namespace GDLevels.Data.Adapters.Interfaces
{
    public interface ILevelsDataAdapter
    {
        public IEnumerable<Level> GetLevelsOnPage(int page, int levelsPerPage);
        public Task<StatusMessage> AddLevelAsync(int levelId);
        public Task<StatusMessage> RemoveLevelAsync(int levelId);
    }
}