﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApp.Models
{
    /// <summary>
    /// Context for planes
    /// </summary>
    [Table("Planes")]
    public class DbPlane
    {
        /// <summary>
        /// Plane ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Model name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Board number
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Number { get; set; }

        /// <summary>
        /// Number of seats
        /// </summary>
        public int SeatsNumber { get; set; }

        /// <summary>
        /// Airline which owns current plane
        /// </summary>
        public int AirlineId { get; set; }
    }
}