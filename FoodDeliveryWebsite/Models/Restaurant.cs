using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [BindNever]
        [ValidateNever]
        public User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
