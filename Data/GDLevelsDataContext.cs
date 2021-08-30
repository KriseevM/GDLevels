using GDLevels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDLevels.Data
{
    public class GDLevelsDataContext : DbContext
    {
        public DbSet<Level> Levels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=levels.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>().HasKey(p => p.ID);
            modelBuilder.Entity<Level>().HasIndex(i => i.LevelID).IsUnique(true);
        }
    }
}
