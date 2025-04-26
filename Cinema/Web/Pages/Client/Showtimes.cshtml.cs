using Application.Interfaces.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Client
{
    public class ShowtimesModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly ISchedulesService _scheduleService;

        public ShowtimesModel(IMovieService movieService, ISchedulesService scheduleService)
        {
            _movieService = movieService;
            _scheduleService = scheduleService;
        }

        public Movie Movie { get; set; }
        public IList<Schedule> Schedules { get; set; }
        public List<DateTime> ShowDates { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieById(id.Value);

            if (Movie == null)
            {
                return NotFound();
            }

            // Get schedules for the next 7 days
            Schedules = await _scheduleService.GetSchedulesForMovie(id.Value);

            // Get unique dates for the date selector
            ShowDates = (await _scheduleService.GetAvailableDatesForMovie(id.Value)).ToList();

            return Page();
        }
    }
}
