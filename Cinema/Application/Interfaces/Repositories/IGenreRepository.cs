using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<Genre?> GetByDescriptionAsync(string desc);
        Task<IEnumerable<Movie>> GetMoviesAsync(int genreId);
        Task<IEnumerable<Movie>> GetMoviesByGenresAsync(IEnumerable<int> genreIds);
    }
}
