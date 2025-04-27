using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IPriceRepository
    {
        Task<IList<ScheduleSeatTypePrice>> GetPricesByScheduleId(int scheduleId);
        Task<MovieBasePrice> GetBasePriceByMovieId(int movieId);
        Task<IList<MovieBasePrice>> GetBasePricesByMovieId(int movieId);
    }
}
