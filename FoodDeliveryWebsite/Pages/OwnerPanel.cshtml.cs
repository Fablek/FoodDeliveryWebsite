using FoodDeliveryWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FoodDeliveryWebsite.Models;

namespace FoodDeliveryWebsite.Pages
{
    [Authorize]
    public class OwnerPanelModel : PageModel
    {
        private readonly AppDbContext _context;

        public string OwnerName { get; set; } = "Owner";
        public List<Restaurant> Restaurants { get; set; } = new();

        public OwnerPanelModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleClaim != "Owner")
            {
                return Forbid();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            OwnerName = user?.Username ?? "Owner";
            Restaurants = await _context.Restaurants
                    .Where(r => r.UserId == user.UserId)
                    .ToListAsync();

            return Page();
        }
    }
}
