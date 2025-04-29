using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace FoodDeliveryWebsite.Helpers
{
    public static class CartHelper
    {
        private const string CartKey = "Cart";

        public static List<CartItem> GetCart(ISession session)
        {
            return session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
        }

        public static void SaveCart(ISession session, List<CartItem> cart)
        {
            session.SetObject(CartKey, cart);
        }

        public static void AddToCart(ISession session, CartItem item)
        {
            var cart = GetCart(session);

            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existing != null)
            {
                existing.Quantity += item.Quantity;
            }
            else
            {
                cart.Add(item);
            }

            SaveCart(session, cart);
        }

        public static void RemoveFromCart(ISession session, int productId)
        {
            var cart = GetCart(session);
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(session, cart);
            }
        }

        public static void ClearCart(ISession session)
        {
            SaveCart(session, new List<CartItem>());
        }
    }
}
