using System.ComponentModel.DataAnnotations;

namespace GDLevels.Models
{
    public class Level
    {
        /// <summary>
        /// ID реквеста
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата добавления уровня в базу данных
        /// </summary>
        public double RequestTime { get; set; }

        /// <summary>
        /// ID предложенного уровня
        /// </summary>
        [Range(10000000, 99999999)]
        public int LevelId { get; set; }
    }
}