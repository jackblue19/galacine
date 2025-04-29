using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    public class ItemsModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public ItemsModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<dynamic> Items { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _dashboardService.GetAllItemsSoldAsync();
        }
    }

}
