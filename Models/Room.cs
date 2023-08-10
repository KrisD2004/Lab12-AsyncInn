using System.ComponentModel.DataAnnotations;
namespace Lab2_AysncInn.Models
{
    public class Room
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Layout { get; set; }

        public List<HotelRoom> hotelRooms { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }

    }
}
