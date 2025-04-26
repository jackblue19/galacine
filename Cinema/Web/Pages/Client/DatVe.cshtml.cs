using Application.Interfaces.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Client
{
    public class DatVeModel : PageModel
    {
        private readonly IMovieService _movieService;

        public DatVeModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IList<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            // Get only active movies that are currently showing using the service
            Movies = await _movieService.GetNowShowingMovies();
        }
    }
}
