using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class OrderAddon
    {
        public int OrderAddonId { get; set; }

        [ForeignKey("OrderDetailId")]
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }

        [ForeignKey("AddonId")]
        public int AddonId { get; set; }
        public Addon Addon { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
