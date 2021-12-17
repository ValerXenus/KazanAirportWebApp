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
        public DbSet<DbAirline> Airlines { get; set; }

        public DbSet<DbCity> Cities { get; set; }

        public DbSet<DbFlight> Flights { get; set; }

        public DbSet<DbPassenger> Passengers { get; set;}

        public DbSet<DbPlane> Planes { get; set; }

        public DbSet<DbUser> Users { get; set; }

        public KazanAirportDbContext() 
            : base(ConfigurationManager.ConnectionStrings["KazanAirportDbContext"].ConnectionString)
        { }
    }
}