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
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly DbSet<MovieGenre> _movieGenreSet;

        public GenreRepository(CinemaDbContext context) : base(context)
        {
            _movieGenreSet = context.Set<MovieGenre>();
        }

        public async Task<Genre?> GetByDescriptionAsync(string desc)
            => string.IsNullOrWhiteSpace(desc)
                ? null
                : await _dbSet.FirstOrDefaultAsync(g => g.GenreDesc.ToLower() == desc.ToLower());

        public async Task<IEnumerable<Movie>> GetMoviesAsync(int genreId)
            => await _movieGenreSet.Where(mg => mg.GenreId == genreId)
                                   .Include(mg => mg.Movie)
                                   .Select(mg => mg.Movie)
                                   .ToListAsync();
        public async Task<IEnumerable<Movie>> GetMoviesByGenresAsync(IEnumerable<int> genreIds)
        {
            if (genreIds == null || !genreIds.Any()) return new List<Movie>();

            return await _movieGenreSet.Where(mg => genreIds.Contains(mg.GenreId))
                                       .Include(mg => mg.Movie)
                                       .Select(mg => mg.Movie)
                                       .Distinct()
                                       .ToListAsync();
        }
    }

}
