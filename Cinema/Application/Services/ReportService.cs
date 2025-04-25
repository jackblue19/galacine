using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReportService
    {
        private readonly CinemaDbContext _context;

        public ReportService(CinemaDbContext context)
        {
            _context = context;
        }

        // Tổng số người dùng
        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        // Tổng số hóa đơn
        public async Task<int> GetTotalBillsAsync()
        {
            return await _context.Bills.CountAsync();
        }

        // Tổng số phim đang chiếu
        public async Task<int> GetActiveMoviesAsync()
        {
            return await _context.Movies
                .Where(m => m.MovieStatus == "Active")  
                .CountAsync();
        }

        // Số phòng đang hoạt động
        public async Task<int> GetTotalRoomsAsync()
        {
            return await _context.Rooms.CountAsync();
        }

        // Tổng số dịch vụ đã tạo
        public async Task<int> GetTotalServicesAsync()
        {
            return await _context.Services.CountAsync();
        }

        // Tổng số vé theo từng loại
        public async Task<Dictionary<string, int>> GetTicketTypeStatsAsync()
        {
            return await _context.Tickets
                .GroupBy(t => t.TicketType)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .ToDictionaryAsync(x => x.Type, x => x.Count);  
        }
    }
}
