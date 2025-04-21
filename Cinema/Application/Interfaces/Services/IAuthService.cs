using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<User?> LoginAsync(string username, string password);
        Task<User?> LoginWithGoogleAsync(string googleToken);
        Task<bool> IsEmailExistAsync(string email);
        Task<bool> CreateStaffAccAsync(string usn, string email, string pwd);
    }
}
