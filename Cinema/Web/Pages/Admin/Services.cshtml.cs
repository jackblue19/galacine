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
    }

}
