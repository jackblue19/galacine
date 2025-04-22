using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Auth;

public class LoginModel : PageModel
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public LoginModel(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _authService.LoginAsync(Username, Password);
        if (user == null)
        {
            ErrorMessage = "Tài khoản hoặc mật khẩu không đúng.";
            return Page();
        }

        // TODO: Thiết lập session / claims / redirect
        // no claims - basic auth
        HttpContext.Session.SetString("user", user.Username);

        var userWithRole = await _userService.GetWithRoleAsync(user.UserId);
        if (userWithRole == null) return Page();

        HttpContext.Session.SetString("role", userWithRole.Role.RoleDesc);

        if (userWithRole.Role.RoleDesc.ToLower() == "customer") return RedirectToPage("/client/index");

        return RedirectToPage("/admin/index");
    }
}
