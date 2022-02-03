using GDLevels.Models;
using Microsoft.EntityFrameworkCore;

namespace GDLevels.Data
{
    public class GDLevelsDataContext : DbContext
    {
        public DbSet<Level> Levels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=levels.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>().HasAlternateKey(i => i.LevelId);
        }
    }
}