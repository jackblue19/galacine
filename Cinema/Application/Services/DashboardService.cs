using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService
    {
        private readonly CinemaDbContext _context;

        public DashboardService(CinemaDbContext context)
        {
            _context = context;
        }

        // Tổng doanh thu từ các hóa đơn đã thanh toán
        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Bills
                .Where(b => b.IsPaid.HasValue && b.IsPaid.Value)  
                .SumAsync(b => (decimal?)b.TotalCost) ?? 0;
        }

        // Tổng số vé đã bán
        public async Task<int> GetTotalTicketsSoldAsync()
        {
            return await _context.Tickets
                .Where(t => t.TicketStatus == "Used" || t.TicketStatus == "Active")  
                .CountAsync();
        }

        // Doanh thu theo ngày
       
        public async Task<Dictionary<string, decimal>> GetRevenueByDayAsync()
        {
            return await _context.Bills
                .Where(b => b.IsPaid.HasValue && b.IsPaid.Value)  
                .GroupBy(b => b.BillDateTime.HasValue ? b.BillDateTime.Value.Date : DateTime.MinValue)  
                .Select(g => new
                {
                    Day = g.Key.ToString("yyyy-MM-dd"),
                    Revenue = g.Sum(b => b.TotalCost)
                })
                .ToDictionaryAsync(x => x.Day, x => x.Revenue);
        }


        // Doanh thu theo phim
        public async Task<Dictionary<string, decimal>> GetRevenueByMovieAsync()
        {
            return await _context.Tickets
                .Include(t => t.Schedule)
                    .ThenInclude(s => s.Movie)
                .Where(t => t.TicketStatus == "Used" || t.TicketStatus == "Active")  // Fixed string comparison
                .GroupBy(t => t.Schedule.Movie.MovieName)
                .Select(g => new
                {
                    Movie = g.Key,
                    Revenue = g.Sum(t => t.TicketPrice)
                })
                .ToDictionaryAsync(x => x.Movie, x => x.Revenue);
        }
    }
}
