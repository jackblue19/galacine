using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Application.Interfaces.Services;

namespace Web.Pages.Manager.MovieManager;

public class IndexModel : PageModel
{
    private readonly IMovieService _movieService;

    public IndexModel(IMovieService movieService)
    {
        _movieService = movieService;
    }

    public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();

    public async Task OnGetAsync()
    {
        Movies = await _movieService.GetAllAsync();
    }
}
