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

        public async Task<Movie> CreateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();

            if (detail is not null)
            {
                detail.MovieId = movie.MovieId;
                await _dbContext.MovieDetails.AddAsync(detail);
            }

            if (genreIds is not null && genreIds.Any())
            {
                foreach (var gid in genreIds)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        $"INSERT INTO MovieGenre(MovieId, GenreId) VALUES({movie.MovieId}, {gid})");
                }
            }

            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> DeleteWithRelationsAsync(int movieId)
        {
            var movie = await _dbContext.Movies.FindAsync(movieId);
            if (movie == null) return false;

            var detail = await _dbContext.MovieDetails.FirstOrDefaultAsync(md => md.MovieId == movieId);
            if (detail != null)
                _dbContext.MovieDetails.Remove(detail);

            var movieGenres = _dbContext.Set<Dictionary<string, object>>("MovieGenre")
                .Where(mg => (int)mg["MovieId"] == movieId);
            _dbContext.RemoveRange(movieGenres);

            _dbContext.Movies.Remove(movie);
            return await _dbContext.SaveChangesAsync() > 0;
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

        public async Task<Movie> UpdateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds)
        {
            var existing = await _dbContext.Movies
                                    .Include(m => m.MovieDetails)
                                    //.Include(m => m.Genres)
                                    .FirstOrDefaultAsync(m => m.MovieId == movie.MovieId);

            if (existing == null) throw new Exception("Movie not found");

            _dbContext.Entry(existing).CurrentValues.SetValues(movie);

            if (detail != null)
            {
                var existingDetail = await _dbContext.MovieDetails.FirstOrDefaultAsync(d => d.MovieId == movie.MovieId);
                if (existingDetail != null)
                    _dbContext.Entry(existingDetail).CurrentValues.SetValues(detail);
                else
                    await _dbContext.MovieDetails.AddAsync(detail);
            }

            // Remove old genres
            var oldGenres = _dbContext.Set<Dictionary<string, object>>("MovieGenre")
                .Where(mg => (int)mg["MovieId"] == movie.MovieId);
            _dbContext.RemoveRange(oldGenres);

            // Add new genres
            if (genreIds != null)
            {
                foreach (var gid in genreIds)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        $"INSERT INTO MovieGenre(MovieId, GenreId) VALUES({movie.MovieId}, {gid})");
                }
            }

            await _dbContext.SaveChangesAsync();
            return movie;
        }
    }
}
