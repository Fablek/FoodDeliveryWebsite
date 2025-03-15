using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int Rating { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
