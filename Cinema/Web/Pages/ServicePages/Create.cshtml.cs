using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.ServicesPages;

public class CreateModel : PageModel
{
    private readonly IServiceService _serviceService;

    public CreateModel(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [BindProperty]
    public CreateServiceDto ServiceDto { get; set; } = new();

    public void OnGet()
    {
        // Nothing to initialize yet
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        ServiceDto.CreatedBy = 1; // Replace with current user ID if using authentication
        await _serviceService.CreateAsync(ServiceDto);
        return RedirectToPage("Index");
    }
}
