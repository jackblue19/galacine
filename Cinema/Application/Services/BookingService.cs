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
        public class BookingService : IBookingService
        {
            private readonly IBookingRepository _bookingRepository;
            private readonly ISeatRepository _seatRepository;
            private readonly IScheduleRepository _scheduleRepository;
            private readonly IPriceRepository _priceRepository;

            public BookingService(
                IBookingRepository bookingRepository,
                ISeatRepository seatRepository,
                IScheduleRepository scheduleRepository,
                IPriceRepository priceRepository)
            {
                _bookingRepository = bookingRepository;
                _seatRepository = seatRepository;
                _scheduleRepository = scheduleRepository;
                _priceRepository = priceRepository;
            }

            public async Task<Bill> GetBookingDetails(int billId)
            {
                return await _bookingRepository.GetBillById(billId);
            }

            public async Task<IList<Ticket>> GetTicketsForBill(int billId)
            {
                return await _bookingRepository.GetTicketsByBillId(billId);
            }

            public async Task<IList<Seat>> GetSeatsForTickets(IList<Ticket> tickets)
            {
                return tickets.Select(t => t.Seat).ToList();
            }

            public async Task<Bill> CreateBooking(int scheduleId, List<int> selectedSeatIds, decimal totalCost)
            {
                // Get or create a default customer user
                var user = await _bookingRepository.GetDefaultCustomerUser();
                if (user == null)
                {
                    user = await _bookingRepository.CreateDefaultCustomerUser();
                }

                // Create the bill
                var bill = new Bill
                {
                    UserId = user.UserId,
                    BillDateTime = DateTime.Now,
                    BillType = "Online Payment",
                    BillStatus = "Pending",
                    TotalCost = totalCost,
                    FinalCost = totalCost,
                    IsPaid = false
                };

                bill = await _bookingRepository.CreateBill(bill);

                // Get the schedule
                var schedule = await _scheduleRepository.GetScheduleById(scheduleId);

                // Get seat prices
                var seatPrices = await _priceRepository.GetPricesByScheduleId(scheduleId);

                // If no specific prices, get base prices
                if (!seatPrices.Any())
                {
                    var basePrices = await _priceRepository.GetBasePricesByMovieId(schedule.MovieId);

                    seatPrices = basePrices.Select(bp => new ScheduleSeatTypePrice
                    {
                        ScheduleId = scheduleId,
                        SeatType = bp.SeatType,
                        Price = bp.BasePrice
                    }).ToList();
                }

                // Create tickets for each selected seat
                foreach (var seatId in selectedSeatIds)
                {
                    var seat = await _seatRepository.GetSeatById(seatId);

                    // Get price for this seat type
                    var price = seatPrices.FirstOrDefault(p => p.SeatType == seat.SeatType)?.Price ?? 90000;

                    var ticket = new Ticket
                    {
                        TicketType = seat.SeatType == "VIP" ? "VIP" : "Standard",
                        TicketStatus = "Active",
                        TicketDateTime = DateTime.Now,
                        TicketPrice = price,
                        SeatId = seatId,
                        ScheduleId = scheduleId,
                        BillId = bill.BillId,
                        UserId = user.UserId 
                    };

                    await _bookingRepository.CreateTicket(ticket);
                }

                return bill;
            }
        }
}
