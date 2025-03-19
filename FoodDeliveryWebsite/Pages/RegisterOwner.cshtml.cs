using FoodDeliveryWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryWebsite.Pages
{
    public class RegisterOwnerModel : PageModel
    {
        private readonly UserService _userService;

        public RegisterOwnerModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _userService.RegisterOwnerAsync(Username, Email, Password))
            {
                return RedirectToPage("/Login");
            }

            Message = "A user with this email already exists.";
            return Page();
        }
    }
}
