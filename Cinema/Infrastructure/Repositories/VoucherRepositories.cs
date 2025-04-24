using Application.Interfaces.Repositories;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Infrastructure.Repositories
{
    public class VoucherRepository : GenericRepository<Voucher>, IVoucherRepository
    {
        private readonly CinemaDbContext _context;

        public VoucherRepository(CinemaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Voucher?> GetByCodeAsync(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }

        public async Task<IEnumerable<Voucher>> GetActiveAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            return await _context.Vouchers
                .Where(v => v.IsActive == true && v.ExpiredDate >= today)
                .ToListAsync();
        }


        public async Task<bool> IsExpired(int voucherId)
        {
            var voucher = await _context.Vouchers.FindAsync(voucherId);
            if (voucher == null) return true;

            var today = DateOnly.FromDateTime(DateTime.Today);
            return voucher.ExpiredDate < today;
        }

    }
}
