using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SchedulesService(IScheduleRepository _scheduleRepository,IScheduleSeatTypePriceRepository _seatTypePriceRepository) : ISchedulesService
    {
        
        public async Task<Schedule> AddAsync(Schedule entity)
        {
            var result = await _scheduleRepository.AddAsync(entity);
            return result;
        }
        public async Task<Schedule> UpdateAsync(Schedule entity)
        {
            var result = await _scheduleRepository.UpdateAsync(entity);
            return result;
        }
        public async Task<Schedule> GetSchedulesById(int id)
        {
            return await _scheduleRepository.GetByIdAsync(id);           
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            var schedules = await _scheduleRepository.GetAllAsync(s => s.Movie, s => s.Room);
            return schedules.OrderBy(s => s.StartDatetime);
        }
        public async Task<bool> HasOverlapAsync(Schedule newSchedule)
        {
            var existingSchedules = await _scheduleRepository.GetAllAsync();
            return existingSchedules.Any(s =>
                s.MovieId == newSchedule.MovieId &&
                s.RoomId == newSchedule.RoomId &&
                s.StartDatetime < newSchedule.EndDatetime &&
                s.EndDatetime > newSchedule.StartDatetime);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _scheduleRepository.GetByIdAsync(id);
            if (schedule == null)
            {
                return false;
            }

            // Kiểm tra nếu lịch chiếu thuộc ngày hiện tại
            var today = DateTime.Today; // Ngày hiện tại: 22/04/2025
            if (schedule.StartDatetime.Date == today)
            {
                throw new InvalidOperationException("Không thể xóa lịch chiếu của ngày hiện tại.");
            }

            return await _scheduleRepository.DeleteAsync(id);
        }
        public async Task<IList<Schedule>> GetSchedulesForMovie(int movieId)
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(7);

            return await _scheduleRepository.GetSchedulesByMovieId(movieId, startDate, endDate);
        }

        public async Task<Schedule> GetScheduleById(int id)
        {
            return await _scheduleRepository.GetScheduleById(id);
        }

        public async Task<IList<DateTime>> GetAvailableDatesForMovie(int movieId)
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(7);

            return await _scheduleRepository.GetDistinctDatesForMovie(movieId, startDate, endDate);
        }
        public async Task AddScheduleSeatTypePricesAsync(int scheduleId, decimal basePrice)
        {
            // Định nghĩa các loại ghế và giá
            var seatTypePrices = new List<(string SeatType, decimal Price)>
            {
                ("Single", basePrice * 1.0m),
                ("VIP", basePrice * 1.5m),
                ("Couple", basePrice * 2.0m)
            };

            // Tạo và thêm các bản ghi ScheduleSeatTypePrice
            foreach (var (seatType, price) in seatTypePrices)
            {
                // Tạo entity ScheduleSeatTypePrice với các thuộc tính phù hợp
                var seatTypePrice = new ScheduleSeatTypePrice
                {
                    ScheduleId = scheduleId,
                    SeatType = seatType,
                    Price = price
                };

                await _seatTypePriceRepository.AddAsync(seatTypePrice);
            }
        }

        public async Task UpdateScheduleSeatTypePricesAsync(int scheduleId, decimal basePrice)
        {
            // Xóa các bản ghi cũ
            var existingPrices = await _seatTypePriceRepository.GetByScheduleIdAsync(scheduleId);
            foreach (var price in existingPrices)
            {
                await _seatTypePriceRepository.DeleteAsync(price.Id);
            }

            // Thêm các bản ghi mới
            await AddScheduleSeatTypePricesAsync(scheduleId, basePrice);
        }

        public async Task<IEnumerable<ScheduleSeatTypePrice>> GetScheduleSeatTypePricesAsync(int scheduleId)
        {
            return await _seatTypePriceRepository.GetByScheduleIdAsync(scheduleId);
        }

    }
}
    