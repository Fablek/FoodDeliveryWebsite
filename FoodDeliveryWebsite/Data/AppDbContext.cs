using FoodDeliveryWebsite.Models;

namespace FoodDeliveryWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RestaurantHours> RestaurantHours { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Addon> Addons { get; set; }
    }
}
