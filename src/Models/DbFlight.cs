using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApp.Models
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
        /// Date and time of scheduled departure
        /// </summary>
        public DateTime DepartureScheduled { get; set; }

        /// <summary>
        /// Date and time of scheduled arrival
        /// </summary>
        public DateTime ArrivalScheduled { get; set; }

        /// <summary>
        /// Actual date and time of departure
        /// </summary>
        public DateTime? DepartureActual { get; set; }

        /// <summary>
        /// Actual date and time of arrival
        /// </summary>
        public DateTime? ArrivalActual { get; set; }

        /// <summary>
        /// Approximate time on board (in minutes)
        /// </summary>
        public int TimeOnBoard { get; set; }

        /// <summary>
        /// Flight type: 0 (false) - Departure, 1 (true) - Arrival
        /// </summary>
        public bool FlightType { get; set; }

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
        public int StatusId { get; set; }
    }
}