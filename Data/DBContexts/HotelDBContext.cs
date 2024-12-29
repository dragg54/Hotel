using Hotel_Management_API.Entities;
using Hotel_Management_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Data.DBContexts
{
    public class HotelDBContext: DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options): base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }  
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
