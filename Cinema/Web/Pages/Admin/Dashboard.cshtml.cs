// Pages/Admin/Dashboard.cshtml.cs

using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages.Admin
{
    //[Authorize(Roles = "admin,manager,staff")]
    public class DashboardModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public DashboardModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public decimal TotalRevenue { get; set; }
        public int TotalTicketsSold { get; set; }
        public Dictionary<string, decimal> RevenueByDay { get; set; }
        public Dictionary<string, decimal> RevenueByMovie { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy thông tin tổng doanh thu, số vé đã bán và doanh thu theo ngày và phim
            TotalRevenue = await _dashboardService.GetTotalRevenueAsync();
            TotalTicketsSold = await _dashboardService.GetTotalTicketsSoldAsync();
            RevenueByDay = await _dashboardService.GetRevenueByDayAsync();
            RevenueByMovie = await _dashboardService.GetRevenueByMovieAsync();
        }
    }
}
