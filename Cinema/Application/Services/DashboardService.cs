using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

        // ✅ Tổng doanh thu từ các hóa đơn đã thanh toán trong tháng này
        public async Task<decimal> GetTotalRevenueThisMonthAsync()
        {
            var now = DateTime.Now;
            return await _context.Bills
                .Where(b => b.IsPaid == true &&
                            b.BillDateTime.HasValue &&
                            b.BillDateTime.Value.Month == now.Month &&
                            b.BillDateTime.Value.Year == now.Year)
                .SumAsync(b => (decimal?)b.FinalCost) ?? 0;
        }

        // ✅ Tổng số vé đã bán trong tháng này
        public async Task<int> GetTotalTicketsSoldThisMonthAsync()
        {
            var now = DateTime.Now;
            return await _context.Tickets
                .Where(t => (t.TicketStatus == "Used" || t.TicketStatus == "Active") &&
                            t.TicketDateTime.HasValue &&
                            t.TicketDateTime.Value.Month == now.Month &&
                            t.TicketDateTime.Value.Year == now.Year)
                .CountAsync();
        }

        // ✅ Tổng số lượng item đã bán trong tháng này
        public async Task<int> GetTotalItemsSoldThisMonthAsync()
        {
            var now = DateTime.Now;
            return await _context.TicketAddons
                .Where(ta => ta.Ticket != null &&
                             ta.Ticket.TicketDateTime.HasValue &&
                             ta.Ticket.TicketDateTime.Value.Month == now.Month &&
                             ta.Ticket.TicketDateTime.Value.Year == now.Year)
                .SumAsync(ta => (int?)ta.Quantity) ?? 0;
        }

        // ✅ Tổng số lượng người dùng đăng ký trong tháng này
        public async Task<int> GetTotalCustomersThisMonthAsync()
        {
            var now = DateTime.Now;

            var customerRoleId = await _context.Roles
                .Where(r => r.RoleDesc.ToLower() == "customer")
                .Select(r => r.RoleId)
                .FirstOrDefaultAsync();

            return await _context.Users
                .Where(u => u.RoleId == customerRoleId &&
                            u.CreatedAt.HasValue &&
                            u.CreatedAt.Value.Month == now.Month &&
                            u.CreatedAt.Value.Year == now.Year)
                .CountAsync();
        }

        // Doanh thu theo phim để làm sơ đồ
        public async Task<Dictionary<string, decimal>> GetRevenueByMovie1Async()
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

        // Top item bán chạy nhất
        // For Items
        public async Task<List<dynamic>> GetTopItemsAsync()
        {
            var items = await _context.TicketAddons
                .GroupBy(ta => new { ta.ItemId, ta.Item.ItemDesc, ta.Item.Type })
                .Select(g => new
                {
                    g.Key.ItemId,
                    g.Key.ItemDesc,
                    g.Key.Type,
                    TotalSold = g.Sum(x => x.Quantity) ?? 0
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();

            return items.Select(x =>
            {
                dynamic expando = new ExpandoObject();
                expando.ItemId = x.ItemId;
                expando.Name = x.ItemDesc;
                expando.Type = x.Type;
                expando.TotalSold = x.TotalSold;
                return expando;
            }).ToList<dynamic>();
        }

        // For Movies
        public async Task<List<dynamic>> GetTopMoviesAsync()
        {
            var movies = await _context.Tickets
                .Where(t => (t.TicketStatus == "Used" || t.TicketStatus == "Active")
                       && t.Schedule != null
                       && t.Schedule.Movie != null)
                .GroupBy(t => new
                {
                    t.Schedule.Movie.MovieId,
                    t.Schedule.Movie.MovieName,
                    t.Schedule.Movie.Rating
                })
                .Select(g => new
                {
                    g.Key.MovieId,
                    g.Key.MovieName,
                    TicketSold = g.Count(),
                    Rating = g.Key.Rating ?? 0
                })
                .OrderByDescending(x => x.TicketSold)
                .Take(5)
                .ToListAsync();

            return movies.Select(x =>
            {
                dynamic expando = new ExpandoObject();
                expando.MovieId = x.MovieId;
                expando.MovieName = x.MovieName;
                expando.TicketSold = x.TicketSold;
                expando.Rating = x.Rating;
                return expando;
            }).ToList<dynamic>();
        }

        // Những người dùng mới đăng ký
        public async Task<List<User>> GetLatestUsersAsync()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return await _context.Users
                .Include(u => u.Role)
                .Where(u =>
                    u.CreatedAt.HasValue &&
                    u.CreatedAt.Value >= firstDayOfMonth &&
                    u.Role.RoleDesc == "customer"
                )
                .OrderByDescending(u => u.CreatedAt)
                .Take(5)
                .ToListAsync();
        }


        //  doanh thu cho tháng và năm hiện tại
        public async Task<decimal> GetTotalRevenueByMonthAsync()
        {
            var currentMonth = DateTime.Now.Month; 
            var currentYear = DateTime.Now.Year; 

            // Tính tổng doanh thu cho tháng và năm hiện tại
            var totalRevenue = await _context.Bills
                .Where(b => b.BillStatus == "Paid" &&
                            b.BillDateTime.HasValue &&
                            b.BillDateTime.Value.Year == currentYear &&
                            b.BillDateTime.Value.Month == currentMonth)
                .SumAsync(b => b.FinalCost ?? 0);  

            return totalRevenue;
        }
        public class TopServiceDto
        {
            public int ServiceId { get; set; }
            public string ServiceDesc { get; set; }
            public int TotalUsed { get; set; }
        }

        //top services
        public async Task<List<TopServiceDto>> GetTopServicesAsync()
        {
            var topServices = await _context.TicketAddons
                .Include(ta => ta.Service)
                .GroupBy(ta => new { ta.ServiceId, ta.Service.ServiceDesc })
                .Select(g => new TopServiceDto
                {
                    ServiceId = g.Key.ServiceId,
                    ServiceDesc = g.Key.ServiceDesc,
                    TotalUsed = g.Sum(x => x.Quantity) ?? 0
                })
                .OrderByDescending(x => x.TotalUsed)
                .Take(5)
                .ToListAsync();

            return topServices;
        }




    }
}



