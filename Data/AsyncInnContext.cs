using Lab2_AysncInn.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2_AysncInn.Data
{
    public class AsyncInnContext : DbContext
    {
        public DbSet<Amenity> Amenities;
        public DbSet<RoomAmenity> RoomAmenities;
        public DbSet<Room> Rooms;
        public DbSet<HotelRoom> HotelRooms;
        public DbSet<Hotel> Hotels;

        public AsyncInnContext(DbContextOptions<AsyncInnContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Amenity>().HasData(new Amenity
            { ID = 1, Name = "A/C" });

            modelBuilder.Entity<RoomAmenity>().HasData(new RoomAmenity
            {
                ID = 1,
                AmenityID = 1,
                RoomID = 1
            });

            modelBuilder.Entity<Room>().HasData(new Room
            { ID = 1, Layout = 0, Name = "Basic Room" });

            modelBuilder.Entity<Hotel>().HasData(new Hotel
            {
                ID = 1,
                Address = "123 Seasame St",
                City = "Memphis",
                State = "TN",
                Name = "Elmo Hotel",
                Phone = "555-888-8888"
            });

            modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom { ID = 1, HotelID = 1, RoomID = 1, Price = 100.78 });


        }
    }
}
