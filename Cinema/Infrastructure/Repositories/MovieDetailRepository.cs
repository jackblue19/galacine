using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Data.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieDetailRepository : GenericRepository<MovieDetail>, IMovieDetailRepository
    {
        public MovieDetailRepository(CinemaDbContext context) : base(context)
        {
        }

        public async Task<MovieDetail?> GetByMovieAsync(int movieId)
            => await _dbSet.FirstOrDefaultAsync(md => md.MovieId == movieId);
    }
}
