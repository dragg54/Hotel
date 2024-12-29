using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Hotel_Management_API.Enums;

namespace Hotel_Management_API.Entities
{
    public class User: BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Role Role { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
