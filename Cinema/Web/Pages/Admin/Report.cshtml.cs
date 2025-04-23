using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    public class ReportModel : PageModel
    {
        private readonly ReportService _reportService;

        public ReportModel(ReportService reportService)
        {
            _reportService = reportService;
        }

        public int TotalUsers { get; set; }
        public int TotalBills { get; set; }
        public int ActiveMovies { get; set; }
        public int TotalRooms { get; set; }
        public int TotalServices { get; set; }
        public Dictionary<string, int> TicketTypeStats { get; set; }

        public async Task OnGetAsync()
        {
            TotalUsers = await _reportService.GetTotalUsersAsync();
            TotalBills = await _reportService.GetTotalBillsAsync();
            ActiveMovies = await _reportService.GetActiveMoviesAsync();
            TotalRooms = await _reportService.GetTotalRoomsAsync();
            TotalServices = await _reportService.GetTotalServicesAsync();
            TicketTypeStats = await _reportService.GetTicketTypeStatsAsync();
        }
    }
}
