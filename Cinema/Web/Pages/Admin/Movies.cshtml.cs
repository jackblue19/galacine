using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Dynamic;
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

        public async Task<IActionResult> OnPostDeleteAsync(int movieId)
        {
            var success = await _dashboardService.DeleteMovieAsync(movieId);
            if (success)
            {
                TempData["SuccessMessage"] = "Movie deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete movie.";
            }

            return RedirectToPage(); // reload page
        }
    }
}
