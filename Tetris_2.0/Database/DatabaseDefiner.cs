using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_2.Database
{
    internal class DatabaseDefiner : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Scoreboard> scoreboards { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Id)
                .IsUnique();
            modelBuilder.Entity<Scoreboard>()
                .HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($@"Data Source={Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\source\repos\Tetris_2.0\Tetris_2.0\Database\Database.db");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
