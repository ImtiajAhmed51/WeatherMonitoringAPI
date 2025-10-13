using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class WeatherContext : DbContext
    {

        public WeatherContext(): base("name=WeatherContext") { }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        internal static WeatherContext Create()
        {
            return new WeatherContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Country
            modelBuilder.Entity<Country>()
                .Property(c => c.Area)
                .HasPrecision(18, 2);

            // State
            modelBuilder.Entity<State>()
                .Property(s => s.Area)
                .HasPrecision(18, 2);

            // City
            modelBuilder.Entity<City>()
                .Property(c => c.Latitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<City>()
                .Property(c => c.Longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<City>()
                .Property(c => c.Area)
                .HasPrecision(18, 2);
            // Area
            modelBuilder.Entity<Area>()
                .Property(a => a.Latitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Area>()
                .Property(a => a.Longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Area>()
                .Property(a => a.AreaSize)
                .HasPrecision(18, 2);
        }
    }
}
