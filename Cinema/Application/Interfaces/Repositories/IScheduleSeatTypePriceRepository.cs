using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IScheduleSeatTypePriceRepository
    {
        Task<ScheduleSeatTypePrice> AddAsync(ScheduleSeatTypePrice entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ScheduleSeatTypePrice>> GetByScheduleIdAsync(int scheduleId);
    }
}
