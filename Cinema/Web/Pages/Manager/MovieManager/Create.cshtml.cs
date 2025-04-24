using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Data.Entities;
using Application.Interfaces.Services;

namespace Web.Pages.Manager.MovieManager;

public class CreateModel : PageModel
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly ICategoryService _categoryService;

    public CreateModel(IMovieService movieService,
                       IGenreService genreService,
                       ICategoryService categoryService)
    {
        _movieService = movieService;
        _genreService = genreService;
        _categoryService = categoryService;
    }

    [BindProperty] public Movie Movie { get; set; } = new();
    [BindProperty] public MovieDetail Detail { get; set; } = new();
    [BindProperty] public List<int> SelectedGenres { get; set; } = new();

    public List<SelectListItem> GenreList { get; set; } = new();
    public List<SelectListItem> CategoryList { get; set; } = new();

    public async Task OnGetAsync()
    {
        var genres = await _genreService.GetAllAsync();
        GenreList = genres.Select(g => new SelectListItem
                                        { Text = g.GenreDesc, Value = g.GenreId.ToString() })
                          .ToList();
        var categories = await _categoryService.GetAllAsync();
        CategoryList = categories.Select(c => new SelectListItem
                                                { Text = c.CategoryDesc, Value = c.CategoryId.ToString() })
                                  .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid) return Page();

        await _movieService.CreateWithRelationsAsync(Movie, Detail, SelectedGenres);
        return RedirectToPage("Index");
    }
}
