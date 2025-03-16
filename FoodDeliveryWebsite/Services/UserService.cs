using FoodDeliveryWebsite.Data;
using FoodDeliveryWebsite.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace FoodDeliveryWebsite.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Role = UserRole.Client,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }
    }
}
