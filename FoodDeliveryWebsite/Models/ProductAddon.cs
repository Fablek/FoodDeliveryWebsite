using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class ProductAddon
    {
        public int ProductAddonId { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("AddonId")]
        public int AddonId{ get; set; }
        public Addon Addon { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}
