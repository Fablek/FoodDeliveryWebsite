using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Pages
{
    public class DeleteProductModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteProductModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (Product == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == Product.ProductId);

            if (product == null)
                return NotFound();

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var relativePath = product.ImageUrl.Replace("/", Path.DirectorySeparatorChar.ToString()).TrimStart(Path.DirectorySeparatorChar);
                var filePath = Path.Combine(_env.WebRootPath, relativePath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            int restaurantId = product.RestaurantId;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Product deleted!";
            return RedirectToPage("/EditRestaurant", new { id = restaurantId });
        }
    }
}
