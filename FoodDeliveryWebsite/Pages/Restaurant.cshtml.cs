using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Pages
{
    public class RestaurantModel : PageModel
    {
        private readonly AppDbContext _context;

        public Restaurant? Restaurant { get; set; }
        public List<Product> Products { get; set; } = new();

        public RestaurantModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == id);

            if (Restaurant == null)
                return NotFound();

            Products = await _context.Products
                .Where(p => p.RestaurantId == id)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return Page();
        }
    }
}
