using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    // for testing code purpose
    public class GenreFilterDto
    {
        public IEnumerable<int> GenreIds { get; set; } = new List<int>();

        public string? Language { get; set; }
        public string? Nation { get; set; }
        public bool? IsSub { get; set; }
        public double? MinRating { get; set; }

        // Pagination
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
    internal class __
    {
        private readonly DbSet<MovieGenre> _movieGenreSet;
        protected readonly CinemaDbContext _dbContext;
        public __(CinemaDbContext dbContext)
        {
            _movieGenreSet = _dbContext.Set<MovieGenre>();
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenresAsync(GenreFilterDto filter)
        {
            if (filter.GenreIds == null || !filter.GenreIds.Any()) return new List<Movie>();

            var query = _movieGenreSet
                .Where(mg => filter.GenreIds.Contains(mg.GenreId))
                .Include(mg => mg.Movie)
                .Select(mg => mg.Movie)

                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Language))
                query = query.Where(m => m.Language != null && m.Language.ToLower() == filter.Language.ToLower());

            if (!string.IsNullOrWhiteSpace(filter.Nation))
                query = query.Where(m => m.Nation != null && m.Nation.ToLower().Contains(filter.Nation.ToLower()));

            if (filter.IsSub.HasValue)
                query = query.Where(m => m.IsSub == filter.IsSub.Value);

            if (filter.MinRating.HasValue)
                query = query.Where(m => m.Rating >= filter.MinRating.Value);

            return await query
                .Distinct()
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync();
        }

    }
}
