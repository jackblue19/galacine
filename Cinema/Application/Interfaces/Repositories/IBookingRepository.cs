using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<Bill> GetBillById(int id);
        Task<IList<Ticket>> GetTicketsByBillId(int billId);
        Task<Bill> CreateBill(Bill bill);
        Task<Ticket> CreateTicket(Ticket ticket);
        Task SaveChangesAsync();
        Task<User> GetDefaultCustomerUser();
        Task<User> CreateDefaultCustomerUser();
    }
}
