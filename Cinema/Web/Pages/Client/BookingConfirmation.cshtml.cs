using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Application.Interfaces.Services;
namespace Web.Pages.Client
{
    public class BookingConfirmationModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public BookingConfirmationModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public Bill Bill { get; set; }
        public List<Ticket> Tickets { get; set; }
        public Schedule Schedule { get; set; }
        public Movie Movie { get; set; }
        public List<Seat> SelectedSeats { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bill = await _bookingService.GetBookingDetails(id.Value);

            if (Bill == null)
            {
                return NotFound();
            }

            // Get tickets for this bill
            Tickets = (await _bookingService.GetTicketsForBill(Bill.BillId)).ToList();

            if (!Tickets.Any())
            {
                return NotFound();
            }

            // Get schedule and movie from the first ticket
            Schedule = Tickets.First().Schedule;
            Movie = Schedule.Movie;

            // Get selected seats
            SelectedSeats = (await _bookingService.GetSeatsForTickets(Tickets)).ToList();

            return Page();
        }
    }
}




  