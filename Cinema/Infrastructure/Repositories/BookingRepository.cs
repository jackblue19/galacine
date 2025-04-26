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
    public class BookingRepository : IBookingRepository
    {
        private readonly CinemaDbContext _context;

        public BookingRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> GetBillById(int id)
        {
            return await _context.Bills
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.BillId == id);
        }

        public async Task<IList<Ticket>> GetTicketsByBillId(int billId)
        {
            return await _context.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Schedule)
                    .ThenInclude(s => s.Movie)
                .Include(t => t.Schedule)
                    .ThenInclude(s => s.Room)
                .Where(t => t.BillId == billId)
                .ToListAsync();
        }

        public async Task<Bill> CreateBill(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return bill;
        }

        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetDefaultCustomerUser()
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.RoleId == 4); // 4 = customer role
        }

        public async Task<User> CreateDefaultCustomerUser()
        {
            var user = new User
            {
                Username = "customer",
                Password = "password", // In a real app, this would be hashed
                Email = "customer@example.com",
                RoleId = 4,
                AccStatus = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
