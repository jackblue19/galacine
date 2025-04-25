
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

        private (DateTime startDate, DateTime endDate) GetCurrentMonthDateRange()
        {
            // Get the start and end date of the current month
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1); 

            return (startDate, endDate);
        }

        // Get total number of users within a given date range (or current month if no filter)
        public async Task<int> GetTotalUsersAsync(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                (startDate, endDate) = GetCurrentMonthDateRange();
            }

            var query = _context.Users.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(u => u.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(u => u.CreatedAt <= endDate.Value);

            return await query.CountAsync();
        }

        // Get total number of bills within a given date range (or current month if no filter)
        public async Task<int> GetTotalBillsAsync(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
              
                (startDate, endDate) = GetCurrentMonthDateRange();
            }

            var query = _context.Bills.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(b => b.BillDateTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(b => b.BillDateTime <= endDate.Value);

            return await query.CountAsync();
        }

        // Get total number of active movies within a given date range (or current month if no filter)
        public async Task<int> GetActiveMoviesAsync(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                (startDate, endDate) = GetCurrentMonthDateRange();
            }

            var query = _context.Movies.AsQueryable();
            DateOnly startDateOnly = DateOnly.FromDateTime(startDate.Value);
            DateOnly endDateOnly = DateOnly.FromDateTime(endDate.Value);
            query = query.Where(m => m.ReleaseDate >= startDateOnly);
            query = query.Where(m => m.ReleaseDate <= endDateOnly);

            return await query.CountAsync();
        }
      
        // Get total number of services created within a given date range (or current month if no filter)
        public async Task<int> GetTotalServicesAsync(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                (startDate, endDate) = GetCurrentMonthDateRange();
            }

            var query = _context.Services.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(s => s.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.CreatedAt <= endDate.Value);

            return await query.CountAsync();
        }

        // Get total ticket statistics grouped by type within a given date range (or current month if no filter)
        public async Task<Dictionary<string, int>> GetTicketTypeStatsAsync(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                (startDate, endDate) = GetCurrentMonthDateRange();
            }

            var query = _context.Tickets.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(t => t.TicketDateTime >= startDate.Value); 

            if (endDate.HasValue)
                query = query.Where(t => t.TicketDateTime <= endDate.Value); 

            return await query
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
