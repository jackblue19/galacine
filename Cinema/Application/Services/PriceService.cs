using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PriceService : IPriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<IList<ScheduleSeatTypePrice>> GetPricesForSchedule(int scheduleId, int movieId)
        {
            var schedulePrices = await _priceRepository.GetPricesByScheduleId(scheduleId);

            // If no specific prices are set for this schedule, get base prices for the movie
            if (!schedulePrices.Any())
            {
                var basePrices = await _priceRepository.GetBasePricesByMovieId(movieId);

                // Convert base prices to schedule prices
                return basePrices.Select(bp => new ScheduleSeatTypePrice
                {
                    ScheduleId = scheduleId,
                    SeatType = bp.SeatType,
                    Price = bp.BasePrice
                }).ToList();
            }

            return schedulePrices;
        }

        public async Task<IList<PriceDto>> GetPricesForScheduleAsDto(int scheduleId, int movieId)
        {
            var prices = await GetPricesForSchedule(scheduleId, movieId);

            return prices.Select(p => new PriceDto
            {
                Id = p.Id,
                ScheduleId = p.ScheduleId,
                SeatType = p.SeatType,
                PriceMultiplier = p.Price
            }).ToList();
        }

        public async Task<IList<ScheduleSeatTypePriceDto>> GetPriceMultipliersForScheduleAsDto(int scheduleId)
        {
            var prices = await _priceRepository.GetPricesByScheduleId(scheduleId);

            // Chuyển đổi giá thành hệ số (không thay đổi database)
            return prices.Select(p => new ScheduleSeatTypePriceDto
            {
                Id = p.Id,
                ScheduleId = p.ScheduleId,
                SeatType = p.SeatType,
                // Giả định rằng giá trong database là giá tuyệt đối, chúng ta sẽ chuyển đổi thành hệ số
                // Ví dụ: Nếu giá VIP là 120000 và giá Single là 60000, hệ số VIP sẽ là 2.0
                PriceMultiplier = GetMultiplierForSeatType(p.SeatType)
            }).ToList();
        }

        public async Task<MovieBasePrice> GetBasePriceForMovie(int movieId)
        {
            // Lấy giá cơ bản từ bảng MovieBasePrice nếu có
            var basePrice = await _priceRepository.GetBasePriceByMovieId(movieId);

            // Nếu không có, tạo một đối tượng mới với giá mặc định
            if (basePrice == null)
            {
                basePrice = new MovieBasePrice
                {
                    MovieId = movieId,
                    BasePrice = 80000 // Giá mặc định
                };
            }

            return basePrice;
        }

        public async Task<MovieBasePriceDto> GetBasePriceForMovieAsDto(int movieId)
        {
            var basePrice = await GetBasePriceForMovie(movieId);

            return new MovieBasePriceDto
            {
                MovieId = basePrice.MovieId,
                BasePrice = basePrice.BasePrice
            };
        }

        public async Task<IList<SeatTypeDefaultPriceDto>> GetDefaultMultipliersAsDto()
        {
            // Trả về các hệ số mặc định được hard-code thay vì lấy từ database
            return new List<SeatTypeDefaultPriceDto>
            {
                new SeatTypeDefaultPriceDto { Id = 1, SeatType = "Single", DefaultMultiplier = 1.0m },
                new SeatTypeDefaultPriceDto { Id = 2, SeatType = "VIP", DefaultMultiplier = 2.0m },
                new SeatTypeDefaultPriceDto { Id = 3, SeatType = "Couple", DefaultMultiplier = 3.0m },
                new SeatTypeDefaultPriceDto { Id = 4, SeatType = "Standard", DefaultMultiplier = 1.0m }
            };
        }

        public async Task<decimal> CalculateFinalPrice(int movieId, string seatType)
        {
            // Lấy giá cơ bản của phim
            var basePrice = await GetBasePriceForMovie(movieId);
            decimal movieBasePrice = basePrice?.BasePrice ?? 80000; // Giá mặc định nếu không tìm thấy

            // Lấy hệ số cho loại ghế từ giá trị mặc định
            decimal multiplier = GetMultiplierForSeatType(seatType);

            // Tính giá cuối cùng
            return movieBasePrice * multiplier;
        }

        // Helper method to get multiplier for seat type
        private decimal GetMultiplierForSeatType(string seatType)
        {
            switch (seatType)
            {
                case "VIP":
                    return 2.0m;
                case "Couple":
                    return 3.0m;
                case "Single":
                case "Standard":
                default:
                    return 1.0m;
            }
        }
    }
}
