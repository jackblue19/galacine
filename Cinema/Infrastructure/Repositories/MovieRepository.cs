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
            movie.Language = "Updating";
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
                                    .Include(m => m.MovieGenres)
                                    //.Include(m => m.Genres)
                                    .FirstOrDefaultAsync(m => m.MovieId == movie.MovieId);

            if (existing == null) throw new Exception("Movie not found");

            existing.MovieName = movie.MovieName;
            existing.MovieDesc = movie.MovieDesc;
            existing.MovieStatus = movie.MovieStatus;
            existing.Rating = movie.Rating;
            existing.Nation = movie.Nation;
            existing.ReleaseDate = movie.ReleaseDate;
            existing.Language = movie.Language;
            existing.IsSub = movie.IsSub;
            existing.CategoryId = movie.CategoryId;
            existing.Duration = movie.Duration;
            existing.MovieStatus = "Active";

            // Update MovieDetail (based on MovieId not MovieDetailId)
            var existingDetail = await _dbContext.MovieDetails.FirstOrDefaultAsync(x => x.MovieId == movie.MovieId);

            //_dbContext.Entry(existing).CurrentValues.SetValues(movie);

            if (existingDetail == null)
            {
                existingDetail = new MovieDetail
                {
                    MovieId = movie.MovieId,
                    Director = detail.Director,
                    Actors = detail.Actors,
                    AgeLimit = detail.AgeLimit,
                    Language = detail.Language
                };
                await _dbContext.MovieDetails.AddAsync(existingDetail);
            }
            else
            {
                existingDetail.Director = detail.Director;
                existingDetail.Actors = detail.Actors;
                existingDetail.AgeLimit = detail.AgeLimit;
                existingDetail.Language = detail.Language;
            }
            existing.Language = existingDetail.Language;

            // Remove old genres
            /*var oldGenres = _dbContext.Set<Dictionary<string, object>>("MovieGenre")
                .Where(mg => (int)mg["MovieId"] == movie.MovieId);
            _dbContext.RemoveRange(oldGenres);*/
            var oldMovieGenres = await _dbContext.MovieGenres
                                                    .Where(mg => mg.MovieId == movie.MovieId)
                                                    .ToListAsync();

            if (oldMovieGenres.Any())
            {
                _dbContext.MovieGenres.RemoveRange(oldMovieGenres);
            }

            // Add new genres
            /*if (genreIds != null)
            {
                foreach (var gid in genreIds)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        $"INSERT INTO MovieGenre(MovieId, GenreId) VALUES({movie.MovieId}, {gid})");
                }
            }*/
            if (genreIds != null && genreIds.Any())
            {
                var newMovieGenres = genreIds.Select(genreId => new MovieGenre
                {
                    MovieId = movie.MovieId,
                    GenreId = genreId
                });

                await _dbContext.MovieGenres.AddRangeAsync(newMovieGenres);
            }

            await _dbContext.SaveChangesAsync();
            return movie;
        }
        public async Task<IList<Movie>> GetActiveMoviesByCategory(string categoryDesc)
        {
            return await _dbContext.Movies
                .Include(m => m.Category)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Where(m => m.MovieStatus == "Active" && m.Category.CategoryDesc == categoryDesc)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _dbContext.Movies
                .Include(m => m.Category)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }
    }
}
