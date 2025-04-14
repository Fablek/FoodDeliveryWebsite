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

        [BindProperty]
        public IFormFile? LogoFile { get; set; }

        [BindProperty]
        public IFormFile? PhotoFile { get; set; }

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

            var uploadsFolder = Path.Combine("wwwroot", "uploads", "restaurants");
            Directory.CreateDirectory(uploadsFolder);

            if (LogoFile != null)
            {
                var logoFileName = $"logo_{Guid.NewGuid()}{Path.GetExtension(LogoFile.FileName)}";
                var logoPath = Path.Combine(uploadsFolder, logoFileName);
                using var stream = new FileStream(logoPath, FileMode.Create);
                await LogoFile.CopyToAsync(stream);
                Restaurant.LogoPath = $"/uploads/restaurants/{logoFileName}";
            }

            if (PhotoFile != null)
            {
                var photoFileName = $"photo_{Guid.NewGuid()}{Path.GetExtension(PhotoFile.FileName)}";
                var photoPath = Path.Combine(uploadsFolder, photoFileName);
                using var stream = new FileStream(photoPath, FileMode.Create);
                await PhotoFile.CopyToAsync(stream);
                Restaurant.PhotoPath = $"/uploads/restaurants/{photoFileName}";
            }

            Restaurant.UserId = user.UserId;
            _context.Restaurants.Add(Restaurant);
            await _context.SaveChangesAsync();

            Message = "Restaurant added successfully!";
            return Page();
        }
    }
}
