using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IMovieBasePriceRepository : IGenericRepository<MovieBasePrice>
    {
        Task<IEnumerable<MovieBasePrice>> GetByMovieAsync(int movieId);
    }
}
