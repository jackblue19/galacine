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

        // Các thuộc tính cho bộ lọc ngày tháng
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        // Các thông tin báo cáo
        public int TotalUsers { get; set; }
        public int TotalBills { get; set; }
        public int ActiveMovies { get; set; }
        public int TotalRooms { get; set; }
        public int TotalServices { get; set; }
        public Dictionary<string, int> TicketTypeStats { get; set; }

        public async Task OnGetAsync()
        {
            // Lấy báo cáo từ dịch vụ
            TotalUsers = await _reportService.GetTotalUsersAsync(StartDate, EndDate);
            TotalBills = await _reportService.GetTotalBillsAsync(StartDate, EndDate);
            ActiveMovies = await _reportService.GetActiveMoviesAsync(StartDate, EndDate);
            TotalServices = await _reportService.GetTotalServicesAsync(StartDate, EndDate);
            TicketTypeStats = await _reportService.GetTicketTypeStatsAsync(StartDate, EndDate);
        }

    }
}

