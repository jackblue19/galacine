using Application.Interfaces.Repositories;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISchedulesService
    {
        Task<IEnumerable<Schedule>> GetAll();
        Task<Schedule> GetSchedulesById(int id);
        Task<Schedule> AddAsync(Schedule entity);
        Task<Schedule> UpdateAsync(Schedule entity);
        Task<bool> HasOverlapAsync(Schedule entity);
        Task<bool> DeleteAsync(int id);
        Task<IList<Schedule>> GetSchedulesForMovie(int movieId);
        Task<Schedule> GetScheduleById(int id);
        Task<IList<DateTime>> GetAvailableDatesForMovie(int movieId);
    }
}
