using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetByScheduleAsync(int scheduleId);
        Task<IEnumerable<Ticket>> GetByUserAsync(int userId);
        Task<Ticket> GetByAccountIdAsync(int accountId, DateTime? date);
        Task<Ticket> GetByMovieIdAsync(int movieId, DateTime? date);
        Task<Ticket> GetByRoomIdAsync(int roomId, DateTime? date);
        Task<Ticket> GetByScheduleAsync(int scheduleId, DateTime? date);
    }
}
