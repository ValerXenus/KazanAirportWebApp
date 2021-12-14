using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApp.Models
{
    /// <summary>
    /// Cities directory
    /// </summary>
    [Table("Cities")]
    public class DbCities
    {
        /// <summary>
        /// City ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// ICAO code of city airport
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(4)]
        public string IcaoCode { get; set; }

        /// <summary>
        /// IATA code of city airport
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(3)]
        public string IataCode { get; set; }
    }
}