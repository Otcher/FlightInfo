using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightInfo.Models;

namespace FlightInfo.Data
{
    public class FlightInfoContext : DbContext
    {
        public FlightInfoContext (DbContextOptions<FlightInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasOne(f => f.Origin);
            modelBuilder.Entity<Flight>().HasOne(a => a.Destination);
            modelBuilder.Entity<City>()
            .HasOne(a => a.Airport)
            .WithOne(b => b.City)
            .HasForeignKey<Airport>(b => b.CityId);
        }

        public DbSet<Models.Airport> Airport { get; set; }

        public DbSet<Models.City> City { get; set; }

        public DbSet<Models.Country> Country { get; set; }

        public DbSet<Models.Flight> Flight { get; set; }

        public DbSet<Models.Passenger> Passenger { get; set; }

        public DbSet<Models.Person> Person { get; set; }

        public DbSet<Models.Pilot> Pilot { get; set; }

        public DbSet<Models.Plane> Plane { get; set; }

        public DbSet<Models.User> User { get; set; }
    }
}
