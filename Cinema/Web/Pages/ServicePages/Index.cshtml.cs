using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.ServicesPages;

public class IndexModel : PageModel
{
    private readonly IServiceService _serviceService;

    public IndexModel(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public List<ServiceDto> Services { get; set; } = new();

    public async Task OnGetAsync()
    {
        Services = await _serviceService.GetAllAsync();
    }
    public async Task<IActionResult> OnPostApproveAsync(int id)
    {
        int adminUserId = 1; // Replace this with actual logged-in admin's ID if available
        await _serviceService.ApproveAsync(id, adminUserId);
        return RedirectToPage();
    }
}
