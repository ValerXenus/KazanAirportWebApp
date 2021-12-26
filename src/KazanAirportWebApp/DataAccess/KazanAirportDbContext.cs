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
        /// <summary>
        /// Авиакомпании
        /// </summary>
        public DbSet<DbAirline> Airlines { get; set; }

        /// <summary>
        /// Города (аэропорты)
        /// </summary>
        public DbSet<DbCity> Cities { get; set; }

        /// <summary>
        /// Рейсы
        /// </summary>
        public DbSet<DbFlight> Flights { get; set; }

        /// <summary>
        /// Информация о пассажирах
        /// </summary>
        public DbSet<DbPassenger> Passengers { get; set;}

        /// <summary>
        /// Самолеты
        /// </summary>
        public DbSet<DbPlane> Planes { get; set; }

        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<DbUser> Users { get; set; }

        /// <summary>
        /// Билеты
        /// </summary>
        public DbSet<DbTicket> Tickets { get; set; }

        public KazanAirportDbContext() 
            : base(ConfigurationManager.ConnectionStrings["KazanAirportDbContext"].ConnectionString)
        { }
    }
}