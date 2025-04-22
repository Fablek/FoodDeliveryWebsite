using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryWebsite.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public List<Restaurant> PopularRestaurants { get; set; } = new();

    public async Task OnGetAsync()
    {
        PopularRestaurants = await _context.Restaurants
            .OrderBy(r => Guid.NewGuid())
            .Take(6)
            .ToListAsync();
    }
}
