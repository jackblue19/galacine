using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(CinemaDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string? email)
            => string.IsNullOrWhiteSpace(email)
            ? null
            : await _dbSet.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

        public async Task<User?> GetByGoogleIdAsync(string googleId)
            => string.IsNullOrEmpty(googleId)
            ? null
            : await _dbSet.FirstOrDefaultAsync(g => g.VerificationCode == googleId);

        public async Task<IEnumerable<User>> GetByNameAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await _dbSet.ToListAsync();
            var lowerName = name.ToLower();
            return await _dbSet.Where(u => (u.FirstName != null && u.FirstName.ToLower().Contains(lowerName))
                                        || (u.LastName != null && u.LastName.ToLower().Contains(lowerName)))
                               .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByPhoneAsync(string? phone)
        => (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone))
            ? new List<User>()
            : await _dbSet.Where(u => u.Phone != null && u.Phone.Contains(phone)).ToListAsync();

        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        => (string.IsNullOrEmpty(role) || string.IsNullOrWhiteSpace(role))
            ? new List<User>()
            : await _dbSet.Include(u => u.Role)
                           .Where(u => u.Role.RoleDesc.ToLower()
                                                       .Contains(role.ToLower()))
                           .ToListAsync();

        public async Task<User?> GetByUsernameAsync(string username)

            => await _dbSet.FirstOrDefaultAsync(u => u.Username == username.ToLower());

        public async Task<User?> GetWithRoleAsync(int userId)
            => await _dbSet.Include(u => u.Role)
                           .FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task<bool> IsEmailExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var lowerEmail = email.ToLower();
            return await _dbSet.AnyAsync(u => u.Email.ToLower() == lowerEmail);
        }

        public async Task<bool> IsUsernameExist(string usn)
        {
            if (string.IsNullOrWhiteSpace(usn)) return false;
            var lowerUsername = usn.ToLower();
            return await _dbSet.AnyAsync(_ => _.Username == lowerUsername);
        }
    }
}
