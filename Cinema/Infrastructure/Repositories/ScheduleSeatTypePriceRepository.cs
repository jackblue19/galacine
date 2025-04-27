using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ScheduleSeatTypePriceRepository : IScheduleSeatTypePriceRepository
    {
        private readonly CinemaDbContext _context;

        public ScheduleSeatTypePriceRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task<ScheduleSeatTypePrice> AddAsync(ScheduleSeatTypePrice entity)
        {
            await _context.ScheduleSeatTypePrices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ScheduleSeatTypePrices.FindAsync(id);
            if (entity == null)
                return false;

            _context.ScheduleSeatTypePrices.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ScheduleSeatTypePrice>> GetByScheduleIdAsync(int scheduleId)
        {
            return await _context.ScheduleSeatTypePrices
                .Where(stp => stp.ScheduleId == scheduleId)
                .ToListAsync();
        }
    }
}
