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
    public class MovieDetailService : GenericService<MovieDetail>, IMovieDetailService
    {
        private readonly IMovieDetailRepository repository;
        public MovieDetailService(IGenericRepository<MovieDetail> repo, IMovieDetailRepository repoz) : base(repo)
        {
            repository = repoz;
        }

        public async Task<MovieDetail?> GetByMovieAsync(int movieId)
            => await repository.GetByMovieAsync(movieId);
    }
}
