using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApp.Models
{
    /// <summary>
    /// Passengers context
    /// </summary>
    [Table("Passengers")]
    public class DbPassengers
    {
        /// <summary>
        /// Passenger ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Middle name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Passport numbers
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string PassportNumber { get; set; }
    }
}