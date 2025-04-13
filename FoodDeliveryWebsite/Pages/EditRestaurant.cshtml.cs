using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Pages
{
    public class EditRestaurantModel : PageModel
    {
        public readonly AppDbContext _context;

        public EditRestaurantModel(AppDbContext context)
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
            {
                return Forbid();
            }

            Restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == id && r.UserId == user.UserId);

            if (Restaurant == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role != UserRole.Owner)
            {
                return Forbid();
            }

            var existing = await _context.Restaurants.FirstOrDefaultAsync(r => r.RestaurantId == Restaurant.RestaurantId && r.UserId == user.UserId);

            if (existing == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            existing.Name = Restaurant.Name;
            existing.Category = Restaurant.Category;
            existing.Address = Restaurant.Address;
            existing.City = Restaurant.City;
            existing.Latitude = Restaurant.Latitude;
            existing.Longitude = Restaurant.Longitude;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Restaurant updated successfully!";
            return RedirectToPage("/OwnerPanel");
        }
    }
}
