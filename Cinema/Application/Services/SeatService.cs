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
    public class SeatService : ISeatService
    {
        private readonly ISeatRepository _seatRepository;

        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<IList<Seat>> GetSeatsForRoom(int roomId)
        {
            return await _seatRepository.GetSeatsByRoomId(roomId);
        }

        public async Task<Seat> GetSeatById(int id)
        {
            return await _seatRepository.GetSeatById(id);
        }

        public async Task<IList<Seat>> GetSeatsWithAvailabilityForSchedule(int roomId, int scheduleId)
        {
            var seats = await _seatRepository.GetSeatsByRoomId(roomId);
            var bookedSeatIds = await _seatRepository.GetBookedSeatIdsForSchedule(scheduleId);

            foreach (var seat in seats)
            {
                if (bookedSeatIds.Contains(seat.SeatId) || seat.SeatStatus != "Available")
                {
                    seat.SeatStatus = "Maintenance"; // Use Maintenance status to indicate booked seats
                }
            }

            return seats;
        }

        // New method to get seats as DTOs to avoid circular references
        public async Task<IList<SeatDto>> GetSeatsWithAvailabilityAsDto(int roomId, int scheduleId)
        {
            var seats = await GetSeatsWithAvailabilityForSchedule(roomId, scheduleId);

            return seats.Select(s => new SeatDto
            {
                SeatId = s.SeatId,
                RoomId = s.RoomId,
                RowNo = s.RowNo,
                ColNo = s.ColNo,
                SeatType = s.SeatType,
                SeatStatus = s.SeatStatus
            }).ToList();
        }

        public async Task<int> GetMaxRowForRoom(int roomId)
        {
            var seats = await _seatRepository.GetSeatsByRoomId(roomId);
            return seats.Any() ? seats.Max(s => s.RowNo) : 0;
        }

        public async Task<int> GetMaxColForRoom(int roomId)
        {
            var seats = await _seatRepository.GetSeatsByRoomId(roomId);
            return seats.Any() ? seats.Max(s => s.ColNo) : 0;
        }
    }
}
