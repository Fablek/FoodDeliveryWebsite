using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FoodDeliveryWebsite.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AddProductModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int restaurantId)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId && r.UserId == user.UserId);

            if (restaurant == null) return Forbid();

            Product = new Product
            {
                RestaurantId = restaurant.RestaurantId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromRoute] int restaurantId)
        {
            Product.RestaurantId = restaurantId;

            if (ImageFile != null)
            {
                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "products");
                Directory.CreateDirectory(uploadsPath);

                var fileName = $"product_{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                Product.ImageUrl = $"/uploads/products/{fileName}";
            }

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    Console.WriteLine($"{entry.Key} - {string.Join(", ", entry.Value.Errors.Select(e => e.ErrorMessage))}");
                }

                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Product added successfully!";
            return RedirectToPage("/EditRestaurant", new { id = Product.RestaurantId });
        }
    }
}
