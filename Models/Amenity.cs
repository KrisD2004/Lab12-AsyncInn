using System.ComponentModel.DataAnnotations;
namespace Lab2_AysncInn.Models
{
    public class Amenity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
