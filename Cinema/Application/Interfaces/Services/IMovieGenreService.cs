using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services;

public interface IMovieGenreService
{
    Task<IEnumerable<int>> GetGenreIdsByMovieIdAsync(int movieId);
}
