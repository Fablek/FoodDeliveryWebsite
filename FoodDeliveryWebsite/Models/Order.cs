using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        public OrderStatuses OrderStatus { get; set; }

        [Required]
        [StringLength(255)]
        public string DeliveryAddress { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
    }

    public enum OrderStatuses
    {
        Placed,
        Prepared,
        Delivered,
        Cancelled,
        NotDelivered,
        Complaint,
        Completed
    }
}
