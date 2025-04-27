using Application.Interfaces.Repositories;
using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SeatRepository :GenericRepository<Seat>, ISeatRepository
    {


        public SeatRepository(CinemaDbContext context) : base(context)
        {
            
        }

        public async Task<IList<Seat>> GetSeatsByRoomId(int roomId)
        {
            return await _dbContext.Seats
                .Where(s => s.RoomId == roomId)
                .ToListAsync();
        }

        public async Task<Seat> GetSeatById(int id)
        {
            return await _dbContext.Seats.FindAsync(id);
        }

        public async Task<IList<int>> GetBookedSeatIdsForSchedule(int scheduleId)
        {
            return await _dbContext.Tickets
                .Where(t => t.ScheduleId == scheduleId && t.TicketStatus != "Cancelled")
                .Select(t => t.SeatId)
                .ToListAsync();
        }

        public Task<IEnumerable<Seat>> GetByRoomAsync(int roomId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Seat>> GetByScheduleAsync(int scheduleId)
        {
            throw new NotImplementedException();
        }
    }
}
