using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_2._0.Model
{
    internal class Scorboard
    {
        public int Id { get; set; }
        public DateTime Spierer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Spielerdatas> Spielerdatas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definieren von Primärschlüsseln für jede Entität
            modelBuilder.Entity<User>().HasKey(k => k.Id);
            NewMethod().HasKey(b => b.Id);
            modelBuilder.Entity<Produkt>().HasKey(p => p.Id);
            modelBuilder.Entity<BestellungDetail>().HasKey(bd => bd.Id);

            // Immer die Basis-Methode aufrufen, um das Basisverhalten einzuschließen
            base.OnModelCreating(modelBuilder);
        }

        private static Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bestellung> NewMethod()
        {
            return modelBuilder.Entity<Bestellung>();
        }

        // OnConfiguring-Methode wird verwendet, um die Datenbankverbindung und andere Konfigurationen einzustellen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Überprüfen, ob der optionsBuilder bereits konfiguriert ist, wenn nicht, konfigurieren
            if (!optionsBuilder.IsConfigured)
            {
                // Konfigurieren der Verwendung der SQLite-Datenbank mit dem angegebenen Verbindungsstring
                optionsBuilder.UseSqlite(@"Data Source=C:\Users\kande\source\repos\csharp-entity-framework-A1-template-lehrkraft\Aufgabe1_GSOPizza\gso_pizza.db");
            }

            // Immer die Basis-Methode aufrufen, um das Basisverhalten einzuschließen
            base.OnConfiguring(optionsBuilder);
        }
    }
}
