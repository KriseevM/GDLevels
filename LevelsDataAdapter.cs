using System;
using System.Collections.Generic;
using GDLevels.Data;
using GDLevels.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GDLevels
{
    public class LevelsDataAdapter : ILevelsDataAdapter
    {
        private GDLevelsDataContext _levelsContext;
        private ILogger<LevelsDataAdapter> _logger;
        public LevelsDataAdapter(ILogger<LevelsDataAdapter> logger, GDLevelsDataContext levelsContext)
        {
            _levelsContext = levelsContext;
            _logger = logger;
        }
        public IEnumerable<Level> GetLevelsOnPage(int page, int levelsPerPage)
        {
            return _levelsContext.Levels.OrderBy(p => p.RequestTime).Reverse().Skip((page - 1) * levelsPerPage).Take(levelsPerPage);
        }

        public async Task<StatusMessage> AddLevelAsync(int levelId)
        {
            if (_levelsContext.Levels.Any(p => (p.LevelID == levelId)))
            {
                return new StatusMessage("Уровень уже есть в базе данных", false);
            }
            _levelsContext.Levels.Add(new Models.Level() { LevelID = levelId, RequestTime = (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds });

            var addedRows = await _levelsContext.SaveChangesAsync();
            if (addedRows == 1)
            {
                _logger.LogInformation("Added level {level} to database", levelId);
                return new StatusMessage("Уровень добавлен в базу данных", true);
            }
            return new StatusMessage("Уровень почему-то не добавлен :(", false);
        }
        
    }

    public class StatusMessage
    {
        private readonly string _message;
        public string Message => _message;
        private readonly bool _isSuccessful = false;
        public bool IsSuccessful => _isSuccessful;

        public StatusMessage(string message, bool isSuccessful)
        {
            _isSuccessful = isSuccessful;
            _message = message;
        }
    }
}