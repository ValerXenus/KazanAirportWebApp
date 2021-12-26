using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApp.Models
{
    /// <summary>
    /// Database class for airlines list
    /// </summary>
    [Table("Airlines")]
    public class DbAirline
    {
        /// <summary>
        /// Airline ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Airline name
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}