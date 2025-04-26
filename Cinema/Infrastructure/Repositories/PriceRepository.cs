using Application.Interfaces.Repositories;
using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly CinemaDbContext _context;

        public PriceRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ScheduleSeatTypePrice>> GetPricesByScheduleId(int scheduleId)
        {
            return await _context.ScheduleSeatTypePrices
                .Where(p => p.ScheduleId == scheduleId)
                .ToListAsync();
        }

        public async Task<MovieBasePrice> GetBasePriceByMovieId(int movieId)
        {
            return await _context.MovieBasePrices
                .FirstOrDefaultAsync(p => p.MovieId == movieId);
        }

        public async Task<IList<MovieBasePrice>> GetBasePricesByMovieId(int movieId)
        {
            // Lấy giá cơ bản của phim theo loại ghế (nếu có)
            return await _context.MovieBasePrices
                .Where(p => p.MovieId == movieId)
                .ToListAsync();
        }
    }
}
