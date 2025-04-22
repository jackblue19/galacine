using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Auth;

public class RegisterModel : PageModel
{
    private readonly IAuthService _authService;

    public RegisterModel(IAuthService authService)
    {
        _authService = authService;
    }

    [BindProperty] public string Username { get; set; } = string.Empty;
    [BindProperty] public string Email { get; set; } = string.Empty;
    [BindProperty] public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (await _authService.IsEmailExistAsync(Email))
        {
            ErrorMessage = "Email đã tồn tại!";
            return Page();
        }

        var success = await _authService.RegisterAsync(Username, Email, Password);
        if (!success)
        {
            ErrorMessage = "Đăng ký thất bại!";
            return Page();
        }

        return RedirectToPage("/auth/login");
    }
}

