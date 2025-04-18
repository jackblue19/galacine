using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        Task<IEnumerable<Bill>> GetByUserAsync(int userId);
        Task<IEnumerable<Bill>> GetUnpaidAsync();
        Task<IEnumerable<Bill>> GetByDatetime(DateTime date);
        Task<bool> IsByCash(int billId);
        Task<Bill> GetByTicketId(int ticketId);
        Task<IEnumerable<Ticket>> GetByBillAsync(int billId);
    }
}
