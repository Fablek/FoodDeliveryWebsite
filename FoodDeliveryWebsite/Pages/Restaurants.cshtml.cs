using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Pages
{
    public class RestaurantsModel : PageModel
    {
        private readonly AppDbContext _context;

        public List<Restaurant> Restaurants { get; set; } = new();

        public RestaurantsModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Restaurants = await _context.Restaurants
                .OrderBy(r => r.Name)
                .ToListAsync();
        }
    }
}