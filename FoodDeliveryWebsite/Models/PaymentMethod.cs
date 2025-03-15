using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryWebsite.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        public MehodType Method { get; set; }

        [Column(TypeName = "text")]
        public string Details { get; set; }
    }

    public enum MehodType
    {
        CreditCard,
        PayPal,
        BankTransfer,
        CashOnDelivery
    }
}
