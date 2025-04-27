using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SchudelesRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public SchudelesRepository(CinemaDbContext context) : base(context)
        {

        }

        public Task<IEnumerable<Schedule>> GetByMovieAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Schedule>> GetUpcomingAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IList<Schedule>> GetSchedulesByMovieId(int movieId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Schedules
                .Include(s => s.Room)
                .Where(s => s.MovieId == movieId && s.StartDatetime >= startDate && s.StartDatetime < endDate)
                .OrderBy(s => s.StartDatetime)
                .ToListAsync();
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return await _dbContext.Schedules
                .Include(s => s.Movie)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(s => s.ScheduleId == id);
        }

        public async Task<IList<DateTime>> GetDistinctDatesForMovie(int movieId, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Schedules
                .Where(s => s.MovieId == movieId && s.StartDatetime >= startDate && s.StartDatetime < endDate)
                .Select(s => s.StartDatetime.Date)
                .Distinct()
                .OrderBy(d => d)
                .ToListAsync();
        }
    }
}
