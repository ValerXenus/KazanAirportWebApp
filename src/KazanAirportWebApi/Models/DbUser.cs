using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KazanAirportWebApi.Models
{
    /// <summary>
    /// Context for Users
    /// </summary>
    [Table("Users")]
    public class DbUser
    {
        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserLogin { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string UserPassword { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string? Email { get; set; }

        /// <summary>
        /// Reference Id to the passenger info
        /// </summary>
        public int? PassengerId { get; set; }

        /// <summary>
        /// User type
        /// </summary>
        public int UserTypeId { get; set; }
    }
}