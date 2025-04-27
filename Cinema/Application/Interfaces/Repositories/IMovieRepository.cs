using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId);
        Task<Movie?> GetWithDetailsAsync(int movieId);
        Task<IEnumerable<Movie>> SearchByNameAsync(string? name);
        Task<IEnumerable<Movie>> GetAllTrailersAsync();
        Task<IEnumerable<Genre>> GetGenresAsync(int movieId);
        Task<Movie> CreateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        Task<Movie> UpdateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        Task<bool> DeleteWithRelationsAsync(int movieId);
        Task<IList<Movie>> GetActiveMoviesByCategory(string categoryDesc);
        Task<Movie> GetMovieById(int id);
    }
}
