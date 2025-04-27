using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISeatRepository : IGenericRepository<Seat>
    {
        Task<IEnumerable<Seat>> GetByRoomAsync(int roomId);
        // Trạng thái ghế theo lịch chiếu (cho Ajax/SignalR)
        //Task<IEnumerable<SeatStatusDto>> GetStatusByScheduleAsync(int scheduleId);

        // Lấy danh sách ghế theo schedule
        Task<IEnumerable<Seat>> GetByScheduleAsync(int scheduleId);
        Task<IList<Seat>> GetSeatsByRoomId(int roomId);
        Task<Seat> GetSeatById(int id);
        Task<IList<int>> GetBookedSeatIdsForSchedule(int scheduleId);
    }
}
