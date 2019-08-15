using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmaiDB.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Filmas> Filmai { get; set; }
        public DbSet<Zanrai> Zanrai { get; set; }
        public DbSet<Aktorius> Aktoriai { get; set; }
        public DbSet<AktoriaiFilmas> AktoriaiFilmai { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filmas>().HasData(
                    new Filmas
                    {
                        Id = 1,
                        Pavadinimas = "Filmas1",
                        IsleidimoData = 2011,
                        //Zanras = Zanras.Drama,
                        //Aktoriai = "Aktorius11, Aktorius12"
                    },
                    new Filmas
                    {
                        Id = 2,
                        Pavadinimas = "Filmas2",
                        IsleidimoData = 2012,
                        //Zanras = Zanras.Komedija,
                        //Aktoriai = "Aktorius21, Aktorius22"
                    }
                );

            modelBuilder.Entity<Zanrai>().HasData(
                    new Zanrai
                    {
                        Id = 1,
                        Pavadinimas = "Drama"
                    },
                    new Zanrai
                    {
                        Id = 2,
                        Pavadinimas = "Komedija"
                    },
                    new Zanrai
                    {
                        Id = 3,
                        Pavadinimas = "Veiksmo"
                    }
                );
        }
    }
}
