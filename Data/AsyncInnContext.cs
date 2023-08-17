using Lab2_AysncInn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab2_AysncInn.Data
{
    public class AsyncInnContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }


        public AsyncInnContext(DbContextOptions<AsyncInnContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // We need Identity to do its thing ...
            base.OnModelCreating(modelBuilder);

            // dummy data tables 
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


            //second dummy data
            modelBuilder.Entity<Amenity>().HasData(new Amenity
            {
                ID = 2,
                Name = "Free Wi-Fi"
            });

            modelBuilder.Entity<RoomAmenity>().HasData(new RoomAmenity
            {
                ID = 2,
                AmenityID = 2,
                RoomID = 2
            });

            modelBuilder.Entity<Room>().HasData(new Room
            {
                ID = 2,
                Layout = 1,
                Name = "Deluxe Room"
            });

            modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom
            {
                ID = 2,
                HotelID = 1,
                RoomID = 2,
                Price = 150.99
            });

            modelBuilder.Entity<Hotel>().HasData(new Hotel
            {
                ID = 2,
                Address = "456 Oak St",
                City = "New York",
                State = "NY",
                Name = "Central Hotel",
                Phone = "555-999-9999"
            });

            //third dummy 
            modelBuilder.Entity<Amenity>().HasData(new Amenity
            {
                ID = 3,
                Name = "Swimming Pool"
            });

            modelBuilder.Entity<RoomAmenity>().HasData(new RoomAmenity
            {
                ID = 3,
                AmenityID = 3,
                RoomID = 3
            });

            modelBuilder.Entity<Room>().HasData(new Room
            {
                ID = 3,
                Layout = 2,
                Name = "Suite Room"
            });

            modelBuilder.Entity<HotelRoom>().HasData(new HotelRoom
            {
                ID = 3,
                HotelID = 3,
                RoomID = 3,
                Price = 180.00
            });


            modelBuilder.Entity<Hotel>().HasData(new Hotel
            {
                ID = 3,
                Address = "789 Pine St",
                City = "Los Angeles",
                State = "CA",
                Name = "Sunset Hotel",
                Phone = "555-777-7777"
            });

            SeedRole(modelBuilder, "Admin", "create", "update", "delete");
            SeedRole(modelBuilder, " Editor", "create", "update");

        }

        public DbSet<Lab2_AysncInn.Models.Hotel> Hotel { get; set; } = default!;

        public DbSet<Lab2_AysncInn.Models.HotelRoom> HotelRoom { get; set; } = default!;

        public DbSet<Lab2_AysncInn.Models.Room> Room { get; set; } = default!;

        public DbSet<Lab2_AysncInn.Models.RoomAmenity> RoomAmenity { get; set; } = default!;

        public DbSet<Lab2_AysncInn.Models.Amenity> Amenity { get; set; } = default!;

        private int nextId = 1;

        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
              new IdentityRoleClaim<string>
              {
                  Id = nextId++,
                  RoleId = role.Id,
                  ClaimType = "permissions", // This matches what we did in Startup.cs
                  ClaimValue = permission
              }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}
