using System.Configuration;
using System.Data.Entity;
using KazanAirportWebApp.Models;

namespace KazanAirportWebApp.DataAccess
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class KazanAirportDbContext : DbContext
    {
        public DbSet<DbAirlines> Airlines { get; set; }

        public DbSet<DbCities> Cities { get; set; }

        public DbSet<DbFlights> Flights { get; set; }

        public DbSet<DbPassengers> Passengers { get; set;}

        public DbSet<DbPlanes> Planes { get; set; }

        public DbSet<DbUsers> Users { get; set; }

        public KazanAirportDbContext() 
            : base(ConfigurationManager.ConnectionStrings["KazanAirportDbContext"].ConnectionString)
        { }
    }
}