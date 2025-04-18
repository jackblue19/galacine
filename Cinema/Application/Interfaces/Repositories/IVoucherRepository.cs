using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IVoucherRepository : IGenericRepository<Voucher>
    {
        Task<Voucher?> GetByCodeAsync(string code);
        Task<IEnumerable<Voucher>> GetActiveAsync();
        Task<bool> IsExpired(int voucherId);
    }
}
