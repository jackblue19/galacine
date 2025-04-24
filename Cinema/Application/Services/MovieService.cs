using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class MovieService : GenericService<Movie>, IMovieService
    {
        private readonly IMovieRepository _movieRepo;
        private readonly IMovieDetailRepository _movieDetailRepo;
        private readonly CinemaDbContext _dbContext;
        public MovieService(IGenericRepository<Movie> repo,
                            IMovieRepository movieService,
                            IMovieDetailRepository movieDetailRepository) : base(repo)
        {
            _movieRepo = movieService;
            _movieDetailRepo = movieDetailRepository;
        }

        public async Task<Movie> CreateAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds)
        {
            var newMovie = await _movieRepo.AddAsync(movie);
            var currentId = newMovie.MovieId;

            if (detail != null)
            {
                detail.MovieId = currentId;
                await _movieDetailRepo.AddAsync(detail);
            }
            if (genreIds != null)
            {
                foreach (var gid in genreIds)
                {
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        $"INSERT INTO MovieGenre(MovieId, GenreId) VALUES({movie.MovieId}, {gid})");
                }
            }

            var res = await GetWithDetailsAsync(currentId);
            if (res == null) return new Movie();
            return res;
        }

        public Task<Movie> ModifyAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetAllTrailersAsync()
            => await _movieRepo.GetAllTrailersAsync();

        public async Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId)
            => await _movieRepo.GetByCategoryAsync(categoryId);

        public async Task<IEnumerable<Genre>> GetGenresAsync(int movieId)
            => await _movieRepo.GetGenresAsync(movieId);

        public async Task<Movie?> GetWithDetailsAsync(int movieId)
            => await _movieRepo.GetWithDetailsAsync(movieId);

        public async Task<IEnumerable<Movie>> SearchByNameAsync(string? name)
            => await _movieRepo.SearchByNameAsync(name);
    }
}
