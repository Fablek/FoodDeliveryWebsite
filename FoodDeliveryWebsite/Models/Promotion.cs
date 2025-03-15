using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class Promotion
    {
        public int PromotionId { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
