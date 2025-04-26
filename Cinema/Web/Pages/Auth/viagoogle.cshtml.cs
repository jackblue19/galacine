using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Pages.Auth;

public class ViagoogleModel : PageModel
{
    private readonly IUserService _userService;

    public ViagoogleModel(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var authResult = await HttpContext.AuthenticateAsync();
        if (!authResult.Succeeded)
        {
            return RedirectToPage("/Auth/Login");
        }

        var email = authResult.Principal.FindFirstValue(ClaimTypes.Email);
        var name = authResult.Principal.FindFirstValue(ClaimTypes.Name) ?? "Unknown";

        var existingUser = (await _userService.GetAllAsync())
            .FirstOrDefault(u => u.Email == email);

        if (existingUser == null)
        {
            var user = new User
            {
                Username = email.Split('@')[0],
                Email = email,
                FirstName = name,
                RoleId = 4, // mặc định là customer
                Password = "GOOGLE-OAUTH", // dummy
                AccStatus = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _userService.AddAsync(user);
            existingUser = user;
        }

        HttpContext.Session.SetString("user", existingUser.Username);
        var userWithRole = await _userService.GetWithRoleAsync(existingUser.UserId);
        HttpContext.Session.SetString("role", userWithRole?.Role?.RoleDesc ?? "customer");

        return userWithRole?.Role?.RoleDesc.ToLower() switch
        {
            "customer" => RedirectToPage("/Client/Index"),
            "admin" or "manager" or "staff" => RedirectToPage("/Admin/Index"),
            _ => RedirectToPage("/Auth/Login")
        };
    }
}
