using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Client;

public class newIndexModel : PageModel
{
    private readonly IMovieService _movieService;

    public newIndexModel(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public List<Movie> ActiveMovies { get; set; } = new();

    public async Task OnGetAsync()
    {
        var movies = await _movieService.GetAllAsync();
        ActiveMovies = movies
                        .Where(m => m.MovieStatus == "Active")
                        .ToList();

        if (ActiveMovies.Count == 0)
        {
            // Nếu rỗng, tự fake 1 phim để debug
            ActiveMovies.Add(new Movie
            {
                MovieName = "Demo Movie",
                MovieImg = "https://i.ebayimg.com/images/g/zRAAAOSwvaJgJSg1/s-l1600.webp"
            });
        }
    }
}
