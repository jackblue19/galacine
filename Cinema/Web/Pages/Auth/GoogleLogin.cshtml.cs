using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Auth;

public class GoogleLoginModel : PageModel
{
    public IActionResult OnGet()
    {
        var redirectUrl = Url.Page("/Auth/Viagoogle");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
}