using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.VisualBasic;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        public AuthService(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }
        public async Task<bool> IsEmailExistAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var user = await _userRepo.GetByEmailAsync(email);
            return user != null;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) return null;
            //if (user.Password == password) return user;
            //else return null;
            return user.Password == password ? user : null;
        }

        public async Task<User?> LoginWithGoogleAsync(string googleToken)
            => await _userRepo.GetByGoogleIdAsync(googleToken);

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            if (await IsEmailExistAsync(email)) return false;
            if (await _userRepo.IsUsernameExist(username)) return false;
            var userRole = await _roleRepo.GetByDescAsync("customer");
            if (userRole == null) return false;
            var newUser = new User
            {
                Email = email,
                Username = username,
                Password = password,
                RoleId = userRole.RoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccStatus = false // temp set to true for testing
            };
            await _userRepo.AddAsync(newUser);
            return true;
        }

        public async Task<bool> StaffRegisterAsync(string usn, string email, string pwd)
        {
            if (await IsEmailExistAsync(email)) return false;
            if (await _userRepo.IsUsernameExist(usn)) return false;
            var staffRole = await _roleRepo.GetByDescAsync("staff");
            if (staffRole == null) return false;
            var newStaff = new User
            {
                Email = email,
                Username = usn,
                Password = pwd,
                RoleId = staffRole.RoleId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                AccStatus = true
            };
            await _userRepo.AddAsync(newStaff);
            return true;
        }
    }
}
