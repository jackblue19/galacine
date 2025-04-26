using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<Bill> GetBookingDetails(int billId);
        Task<IList<Ticket>> GetTicketsForBill(int billId);
        Task<IList<Seat>> GetSeatsForTickets(IList<Ticket> tickets);
        Task<Bill> CreateBooking(int scheduleId, List<int> selectedSeatIds, decimal totalCost);
    }
}
