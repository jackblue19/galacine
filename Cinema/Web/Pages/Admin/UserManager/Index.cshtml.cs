using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.UserManager;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public List<User> Users { get; set; } = new();
    [BindProperty(SupportsGet = true)] public string SortBy { get; set; } = "CreatedAt";
    [BindProperty(SupportsGet = true)] public bool SortAsc { get; set; } = false;

    [BindProperty(SupportsGet = true)] public string? SearchQuery { get; set; }
    [BindProperty(SupportsGet = true)] public DateOnly? FromDate { get; set; }
    [BindProperty(SupportsGet = true)] public DateOnly? ToDate { get; set; }

    private IEnumerable<User> ApplySorting(IEnumerable<User> query)
    {
        return SortBy switch
        {
            "Email" => SortAsc ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
            "Username" => SortAsc ? query.OrderBy(u => u.Username) : query.OrderByDescending(u => u.Username),
            "Name" => SortAsc ? query.OrderBy(u => u.FirstName) : query.OrderByDescending(u => u.FirstName),
            "CreatedAt" => SortAsc ? query.OrderBy(u => u.CreatedAt) : query.OrderByDescending(u => u.CreatedAt),
            _ => query
        };
    }

    public async Task OnGetAsync()
    {
        var allUsers = await _userService.GetAllAsync(u => u.Role);

        var filtered = allUsers.Where(u =>
            string.IsNullOrWhiteSpace(SearchQuery) ||
            u.Email.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
            ($"{u.FirstName} {u.LastName}").Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

        if (FromDate.HasValue)
        {
            var from = FromDate.Value.ToDateTime(TimeOnly.MinValue);
            filtered = filtered.Where(u => u.CreatedAt >= from);
        }

        if (ToDate.HasValue)
        {
            var to = ToDate.Value.ToDateTime(TimeOnly.MaxValue);
            filtered = filtered.Where(u => u.CreatedAt <= to);
        }

        Users = ApplySorting(filtered).ToList();
    }

    public async Task<IActionResult> OnPostSetStatusAsync(int id, bool status)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound();

        user.AccStatus = status;
        await _userService.UpdateAsync(user);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result)
        {
            TempData["ErrorMessage"] = "Xoá thất bại hoặc người dùng không tồn tại!";
        }
        return RedirectToPage();
    }
}
