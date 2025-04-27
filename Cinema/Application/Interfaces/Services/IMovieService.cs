using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Services
{
    public interface IMovieService : IGenericService<Movie>
    {
        //<>
        Task<Movie> CreateAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        Task<Movie> ModifyAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        //Task<IEnumerable<Movie>> GetByGenresAsync(GenreFilterDto filter);
        // </>
        Task<Movie?> GetWithDetailsAsync(int movieId);
        Task<IEnumerable<Movie>> SearchByNameAsync(string? name);
        Task<IEnumerable<Movie>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Movie>> GetAllTrailersAsync();
        Task<IEnumerable<Genre>> GetGenresAsync(int movieId);
        Task<Movie> CreateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        Task<Movie> UpdateWithRelationsAsync(Movie movie, MovieDetail? detail, IEnumerable<int>? genreIds);
        Task<bool> DeleteWithRelationsAsync(int movieId);
        Task<IList<Movie>> GetNowShowingMovies();
        Task<Movie> GetMovieById(int id);
    }
}
