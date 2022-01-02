using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApi.Models
{
    /// <summary>
    /// Flights context
    /// </summary>
    [Table("Flights")]
    public class DbFlight
    {
        /// <summary>
        /// Flight ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Flight number
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string FlightNumber { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) по расписанию
        /// </summary>
        public DateTime ScheduledDateTime { get; set; }

        /// <summary>
        /// Дата и время (вылета/прибытия) реальное
        /// </summary>
        public DateTime? ActualDateTime { get; set; }

        /// <summary>
        /// ID of the plane
        /// </summary>
        public int PlaneId { get; set; }

        /// <summary>
        /// ID of the destination/point of departure
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Status of the current flight
        /// </summary>
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}