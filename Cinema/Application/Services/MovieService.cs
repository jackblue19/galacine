using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Services;
using Data.Entities;

namespace Application.Services
{
    public class MovieService : GenericService<Movie>, IMovieService
    {
        private readonly IMovieService _movieService;
        public MovieService(IGenericRepository<Movie> repo, IMovieService movieService) : base(repo)
        {
            _movieService = movieService;
        }

        public Task<Movie> CreateAsync(Movie movie, MovieDetail detail, IEnumerable<int> genreIds)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetAllTrailersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Genre>> GetGenresAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetWithDetailsAsync(int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> ModifyAsync(Movie movie, MovieDetail detail, IEnumerable<int> genreIds)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> SearchByNameAsync(string? name)
        {
            throw new NotImplementedException();
        }
    }
}
