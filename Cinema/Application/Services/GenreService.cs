using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;

namespace Application.Services
{
    public class GenreService : GenericService<Genre>, IGenreService
    {
        private readonly IGenreRepository _genreRepo;
        public GenreService(IGenericRepository<Genre> repo, IGenreRepository genreRepository) : base(repo)
        {
            _genreRepo = genreRepository;
        }

        public async Task<Genre?> GetByDescriptionAsync(string desc)
            => await _genreRepo.GetByDescriptionAsync(desc);

        public async Task<IEnumerable<Movie>> GetMoviesAsync(int genreId)
            => await _genreRepo.GetMoviesAsync(genreId);

        public async Task<IEnumerable<Movie>> GetMoviesByGenresAsync(IEnumerable<int> genreIds)
            => await _genreRepo.GetMoviesByGenresAsync(genreIds);

    }
}
