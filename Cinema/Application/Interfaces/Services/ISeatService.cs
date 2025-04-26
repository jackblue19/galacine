using Application.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ISeatService
    {
        Task<IList<Seat>> GetSeatsForRoom(int roomId);
        Task<Seat> GetSeatById(int id);
        Task<IList<Seat>> GetSeatsWithAvailabilityForSchedule(int roomId, int scheduleId);
        Task<IList<SeatDto>> GetSeatsWithAvailabilityAsDto(int roomId, int scheduleId);
        Task<int> GetMaxRowForRoom(int roomId);
        Task<int> GetMaxColForRoom(int roomId);
    }
}
