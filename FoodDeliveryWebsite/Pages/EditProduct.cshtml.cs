using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebsite.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditProductModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (Product == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == Product.ProductId);

            if (existing == null)
                return NotFound();

            if (!ModelState.IsValid)
                return Page();

            existing.Name = Product.Name;
            existing.Description = Product.Description;
            existing.Price = Product.Price;
            existing.Category = Product.Category;

            if (ImageFile != null)
            {
                if (!string.IsNullOrEmpty(existing.ImageUrl))
                {
                    var oldRelativePath = existing.ImageUrl.Replace("/", Path.DirectorySeparatorChar.ToString()).TrimStart(Path.DirectorySeparatorChar);
                    var oldFilePath = Path.Combine(_env.WebRootPath, oldRelativePath);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "products");
                Directory.CreateDirectory(uploadsPath);

                var fileName = $"product_{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await ImageFile.CopyToAsync(stream);

                existing.ImageUrl = $"/uploads/products/{fileName}";
            }

            await _context.SaveChangesAsync();

            TempData["Message"] = "Product updated!";
            return RedirectToPage("/EditRestaurant", new { id = existing.RestaurantId });
        }
    }
}
