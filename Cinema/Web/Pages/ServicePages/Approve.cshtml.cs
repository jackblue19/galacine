using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.ServicesPages;

public class ApproveModel : PageModel
{
    private readonly IServiceService _serviceService;

    public ApproveModel(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    public List<ServiceDto> PendingServices { get; set; } = new();

    public async Task OnGetAsync()
    {
        PendingServices = await _serviceService.GetPendingAsync();
    }

    public async Task<IActionResult> OnPostApproveAsync(int id)
    {
        int approvedByUserId = 1; // Replace with actual logged-in user ID in real app
        await _serviceService.ApproveAsync(id, approvedByUserId);
        return RedirectToPage();
    }
}
