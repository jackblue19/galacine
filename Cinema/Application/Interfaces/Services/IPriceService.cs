using Application.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPriceService
    {
        Task<IList<ScheduleSeatTypePrice>> GetPricesForSchedule(int scheduleId, int movieId);
        Task<IList<PriceDto>> GetPricesForScheduleAsDto(int scheduleId, int movieId);
        Task<IList<ScheduleSeatTypePriceDto>> GetPriceMultipliersForScheduleAsDto(int scheduleId);
        Task<MovieBasePrice> GetBasePriceForMovie(int movieId);
        Task<MovieBasePriceDto> GetBasePriceForMovieAsDto(int movieId);
        Task<IList<SeatTypeDefaultPriceDto>> GetDefaultMultipliersAsDto();
        Task<decimal> CalculateFinalPrice(int movieId, string seatType);
    }
}
