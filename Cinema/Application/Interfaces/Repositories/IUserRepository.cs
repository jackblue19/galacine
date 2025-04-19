using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string? email);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetByRoleAsync(string role);
        Task<IEnumerable<User>> GetByNameAsync(string? name);
        Task<IEnumerable<User>> GetByPhoneAsync(string? phone);
        Task<User?> GetByGoogleIdAsync(string googleId);
        Task<User?> GetWithRoleAsync(int userId);
    }
}
