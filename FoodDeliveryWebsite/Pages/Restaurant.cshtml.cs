using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Helpers;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Pages
{
    public class RestaurantModel : PageModel
    {
        private readonly AppDbContext _context;

        public RestaurantModel(AppDbContext context)
        {
            _context = context;
        }

        public Restaurant? Restaurant { get; set; }
        public List<Product> Products { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == id);
            if (Restaurant == null)
                return NotFound();

            Products = await _context.Products
                .Where(p => p.RestaurantId == id)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int ProductId, int Quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == ProductId);
            if (product == null)
                return NotFound();

            var item = new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = Quantity,
                ImageUrl = product.ImageUrl
            };

            CartHelper.AddToCart(HttpContext.Session, item);
            TempData["Message"] = $"{product.Name} added to cart.";

            return RedirectToPage(new { id = product.RestaurantId });
        }
    }
}
