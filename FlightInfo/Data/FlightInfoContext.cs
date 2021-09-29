using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightInfo.Data
{
    public class FlightInfoContext : DbContext
    {
        public FlightInfoContext (DbContextOptions<FlightInfoContext> options) : base(options)
        {

        }

        public DbSet<Models.Airport> Airport { get; set; }

        public DbSet<Models.City> City { get; set; }

        public DbSet<Models.Country> Country { get; set; }

        public DbSet<Models.Flight> Flight { get; set; }

        public DbSet<Models.Passenger> Passenger { get; set; }

        public DbSet<Models.Person> Person { get; set; }

        public DbSet<Models.Pilot> Pilot { get; set; }

        public DbSet<Models.Plane> Plane { get; set; }

    }
}
