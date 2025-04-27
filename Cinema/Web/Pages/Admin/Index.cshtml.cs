using Application.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Application.Services.DashboardService;

namespace Web.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public IndexModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public List<dynamic> TopMovies { get; set; }
        public List<TopServiceDto> TopServices { get; set; }
        public List<dynamic> TopItems { get; set; }
        public List<User> LatestUsers { get; set; } = new();

        public int TotalTicketsSold { get; set; }
        public int TotalItemsSold { get; set; }
        public int TotalCustomers { get; set; }
        public decimal RevenueByMonth { get; set; }

        public async Task OnGetAsync()
        {
            TotalTicketsSold = await _dashboardService.GetTotalTicketsSoldThisMonthAsync();
            TotalItemsSold = await _dashboardService.GetTotalItemsSoldThisMonthAsync();
            TotalCustomers = await _dashboardService.GetTotalCustomersThisMonthAsync();
            RevenueByMonth = await _dashboardService.GetTotalRevenueByMonthAsync();
            TopMovies = await _dashboardService.GetTopMoviesAsync();
            TopServices = await _dashboardService.GetTopServicesAsync();
            TopItems = await _dashboardService.GetTopItemsAsync();
            LatestUsers = await _dashboardService.GetLatestUsersAsync();

        }
    }

}
