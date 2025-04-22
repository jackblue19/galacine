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
    public class SchedulesService(IScheduleRepository _scheduleRepository) : ISchedulesService
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
    }
}
