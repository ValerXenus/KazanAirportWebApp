using KazanAirportWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KazanAirportWebApi.DataAccess
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

        public KazanAirportDbContext(DbContextOptions<KazanAirportDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DbUser>().HasData(
                new DbUser
                {
                    Id = 1,
                    Email = "admin@test.ru",
                    UserLogin = "admin",
                    UserPassword = "698d51a19d8a121ce581499d7b701668", // 111
                    UserTypeId = 2
                });
        }
    }
}