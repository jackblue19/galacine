using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;

namespace Web.Pages.Manager.UserManager
{
    public class IndexModel : PageModel
    {
        private readonly Data.CinemaDbContext _context;

        public IndexModel(Data.CinemaDbContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users
                .Include(u => u.Role).ToListAsync();
        }
    }
}
