using Application.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Pages.Admin
{
    public class MoviesModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public MoviesModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<dynamic> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _dashboardService.GetAllMoviesByTicketSoldAsync();
        }
    }

}
