using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Pages
{
    public class DeleteRestaurantModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteRestaurantModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role != UserRole.Owner)
                return Forbid();

            Restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id && r.UserId == user.UserId);

            if (Restaurant == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role != UserRole.Owner)
                return Forbid();

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == id && r.UserId == user.UserId);

            if (restaurant == null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Restaurant deleted successfully!";
            return RedirectToPage("/OwnerPanel");
        }
    }
}
