using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryWebsite.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum UserRole
    {
        Client,
        Owner
    }
}
