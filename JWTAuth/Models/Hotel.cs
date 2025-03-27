using System.ComponentModel.DataAnnotations;

namespace JWTAuth.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Location { get; set; } = "";

        public int StarRating { get; set; }

        public int NumberOfRooms { get; set; }
    }
}
