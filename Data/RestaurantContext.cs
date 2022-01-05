using rezerviraj.si.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rezerviraj.si.Data
{
    public class RestaurantContext : IdentityDbContext<Restavracija>
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {

        }

        public DbSet<Restavracija> Restavracije { get; set; }
        public DbSet<Gost> Gostje { get; set; }
        public DbSet<Lokacija> Lokacija { get; set; }
        public DbSet<Miza> Miza { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restavracija>().ToTable("Restavracija");
            modelBuilder.Entity<Gost>().ToTable("Gost");
            modelBuilder.Entity<Lokacija>().ToTable("Lokacija");
            modelBuilder.Entity<Miza>().ToTable("Miza");
            modelBuilder.Entity<Rezervacija>().ToTable("Rezervacija");
        }

        public async Task<List<string>> GetDistinctCountires() {
            List<Restavracija> restavracije = await this.Restavracije.ToListAsync();
            List<string> output = new List<string>();

            foreach (Restavracija restavracija in restavracije) {
                if (restavracija.Lokacija != null && !output.Contains(restavracija.Lokacija.Drzava)) {
                    output.Add(restavracija.Lokacija.Drzava);
                }
            }

            return output;
        }

        public async Task<List<string>> GetDistinctCities() {
            List<Restavracija> restavracije = await this.Restavracije.ToListAsync();
            List<string> output = new List<string>();

            foreach (Restavracija restavracija in restavracije) {
                if (restavracija.Lokacija != null && !output.Contains(restavracija.Lokacija.Kraj)) {
                    output.Add(restavracija.Lokacija.Kraj);
                }
            }

            return output;
        }

    }
}