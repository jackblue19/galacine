using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly DbSet<MovieGenre> _movieGenreSet;
        public MovieRepository(CinemaDbContext context) : base(context)
        {
            _movieGenreSet = context.Set<MovieGenre>();
        }

        public async Task<IEnumerable<Movie>> GetAllTrailersAsync()
            => await _dbSet.Where(m => !string.IsNullOrEmpty(m.Trailer)).ToListAsync();

        public async Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId)
            => await _dbSet.Where(m => m.CategoryId == categoryId).ToListAsync();

        public async Task<IEnumerable<Genre>> GetGenresAsync(int movieId)
            => await _movieGenreSet.Where(mg => mg.MovieId == movieId)
                                   .Include(mg => mg.Genre)
                                   .Select(mg => mg.Genre)
                                   .ToListAsync();

        public async Task<Movie?> GetWithDetailsAsync(int movieId)
          => await _dbSet.Include(m => m.Category)
                         .Include(m => m.MovieGenres)
                         .ThenInclude(mg => mg.Genre)
                         .FirstOrDefaultAsync(m => m.MovieId == movieId);

        public async Task<IEnumerable<Movie>> SearchByNameAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await _dbSet.ToListAsync();
            var lower = name.ToLower();
            return await _dbSet.Where(m => m.MovieName.ToLower().Contains(lower)).ToListAsync();
        }
    }
}
