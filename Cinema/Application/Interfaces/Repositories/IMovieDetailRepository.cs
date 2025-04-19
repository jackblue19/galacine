using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMovieDetailRepository : IGenericRepository<MovieDetail>
    {
        Task<MovieDetail?> GetByMovieAsync(int movieId);
    }
}
