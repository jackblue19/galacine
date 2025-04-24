using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.ServicesPages;

public class EditModel : PageModel
{
    private readonly IServiceService _serviceService;

    public EditModel(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [BindProperty]
    public UpdateServiceDto ServiceDto { get; set; } = new();

    public int ServiceId { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        ServiceDto = new UpdateServiceDto
        {
            ServiceDesc = service.ServiceDesc,
            Note = service.Note
        };

        ServiceId = service.ServiceId;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
            return Page();

        await _serviceService.UpdateAsync(id, ServiceDto);
        return RedirectToPage("Index");
    }
}
