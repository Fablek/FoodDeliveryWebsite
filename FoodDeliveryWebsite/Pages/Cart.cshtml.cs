using FoodDeliveryWebsite.Helpers;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FoodDeliveryWebsite.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal Total => Items.Sum(x => x.TotalPrice);

        public void OnGet()
        {
            Items = CartHelper.GetCart(HttpContext.Session);
        }
    }
}
