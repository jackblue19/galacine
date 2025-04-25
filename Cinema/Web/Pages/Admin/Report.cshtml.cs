using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace Web.Pages.Admin
{
    public class ReportModel : PageModel
    {
        private readonly ReportService _reportService;

        public ReportModel(ReportService reportService)
        {
            _reportService = reportService;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

  
        public int TotalUsers { get; set; }
        public int TotalBills { get; set; }
        public int ActiveMovies { get; set; }
        public int TotalRooms { get; set; }
        public int TotalServices { get; set; }
        public Dictionary<string, int> TicketTypeStats { get; set; }
        public decimal TotalRevenue { get; set; }
        public async Task OnGetAsync()
        {
            TotalRevenue = await _reportService.GetTotalRevenueAsync(StartDate, EndDate);

            TotalUsers = await _reportService.GetTotalUsersAsync(StartDate, EndDate);
            TotalBills = await _reportService.GetTotalBillsAsync(StartDate, EndDate);
            ActiveMovies = await _reportService.GetActiveMoviesAsync(StartDate, EndDate);
            TotalServices = await _reportService.GetTotalServicesAsync(StartDate, EndDate);
            TicketTypeStats = await _reportService.GetTicketTypeStatsAsync(StartDate, EndDate);
        }

    }
}

