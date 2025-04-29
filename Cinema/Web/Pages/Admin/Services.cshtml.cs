using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Application.Services.DashboardService;

namespace Web.Pages.Admin
{
    public class ServicesModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public ServicesModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<TopServiceDto> Services { get; set; }

        public async Task OnGetAsync()
        {   
            Services = await _dashboardService.GetAllServicesUsedAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int serviceId)
        {
            var success = await _dashboardService.DeleteServiceAsync(serviceId);
            if (success)
            {
                TempData["SuccessMessage"] = "Services deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Services to delete movie.";
            }

            return RedirectToPage(); // reload page
        }
    }

}
