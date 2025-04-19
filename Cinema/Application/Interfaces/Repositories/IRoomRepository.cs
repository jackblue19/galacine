using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<IEnumerable<Room>> GetWithSeatsAsync();
        Task<IEnumerable<Room>> SearchByNameAsync(string? roomName);
        Task<bool> IsRoomFull(int roomId, DateTime date);
        Task<Room?> GetByIdWithSeatsAsync(int roomId);
        Task<IEnumerable<Seat>> GetAvailableByScheduleAsync(int scheduleId);
    }
}
