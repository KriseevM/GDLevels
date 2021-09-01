using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GDLevels.Models
{
    public class Level
    {
        /// <summary>
        /// ID реквеста
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Дата добавления уровня в базу данных
        /// </summary>
        public double RequestTime{ get; set; }
        /// <summary>
        /// ID предложенного уровня
        /// </summary>
        [Range(10000000, 99999999)]
        public int LevelID { get; set; }
    }
}
