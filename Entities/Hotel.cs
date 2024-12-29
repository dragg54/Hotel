using Hotel_Management_API.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Management_API.Models
{
    public class Hotel: BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public Owner Owner { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string City {get;set;}

        public string State { get; set; }  

        public string PostalCode {get; set; } 

        public int StarRating {get;set;}
    }
}
