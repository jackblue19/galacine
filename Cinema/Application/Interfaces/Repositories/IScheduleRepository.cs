using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<IEnumerable<Schedule>> GetByMovieAsync(int movieId);
        Task<IEnumerable<Schedule>> GetUpcomingAsync();
        Task<IList<Schedule>> GetSchedulesByMovieId(int movieId, DateTime startDate, DateTime endDate);
        Task<Schedule> GetScheduleById(int id);
        Task<IList<DateTime>> GetDistinctDatesForMovie(int movieId, DateTime startDate, DateTime endDate);
    }
}
