using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<int> GetTicketsSoldAsync(DateTime from, DateTime to);
        Task<decimal> GetRevenueAsync(DateTime from, DateTime to);
        //Task<IEnumerable<RevenueByMovieDto>> GetRevenueByMovieAsync(DateTime from, DateTime to);
        Task<IEnumerable<Bill>> GetBillHistoryAsync(int userId, DateTime from, DateTime to);
    }
}
