using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
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
    }
}
