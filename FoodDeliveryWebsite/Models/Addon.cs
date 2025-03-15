using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class Addon
    {
        public int AddonId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}
