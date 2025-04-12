using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Pages
{
    public class AddRestaurantModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddRestaurantModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public string Message { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Login");
            }

            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role != "Owner")
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || user.Role != UserRole.Owner)
            {
                return Forbid();
            }

            Restaurant.UserId = user.UserId;
            _context.Restaurants.Add(Restaurant);
            await _context.SaveChangesAsync();

            Message = "Restaurant added successfully!";
            return Page();
        }
    }
}
