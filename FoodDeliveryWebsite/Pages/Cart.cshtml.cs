using FoodDeliveryWebsite.Helpers;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult OnPostRemove(int productId)
        {
            var cart = CartHelper.GetCart(HttpContext.Session);
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                CartHelper.SaveCart(HttpContext.Session, cart);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostUpdateQuantity(int productId, int quantity)
        {
            var cart = CartHelper.GetCart(HttpContext.Session);
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
                CartHelper.SaveCart(HttpContext.Session, cart);
            }

            return RedirectToPage();
        }
    }
}
