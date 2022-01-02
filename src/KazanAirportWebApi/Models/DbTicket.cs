using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApi.Models
{
    /// <summary>
    /// Класс билетов
    /// </summary>
    [Table("Tickets")]
    public class DbTicket
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Номер билета
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string TicketNumber { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// ID пассажира
        /// </summary>
        public int PassengerId { get; set; }

        /// <summary>
        /// ID рейса
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Номер места в самолете
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(5)]
        public string? SeatNumber { get; set; }
    }
}