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
        Task<Movie> CreateAsync(Movie movie, MovieDetail detail, IEnumerable<int> genreIds);
        Task<Movie> ModifyAsync(Movie movie, MovieDetail detail, IEnumerable<int> genreIds);
        //Task<IEnumerable<Movie>> GetByGenresAsync(GenreFilterDto filter);
    }
}
