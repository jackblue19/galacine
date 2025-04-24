using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Application.Interfaces.Services;
using Application.Services;

namespace Web.Pages.Manager.MovieManager;

public class EditModel : PageModel
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly IMovieGenreService _movieGenreService;
    private readonly ICategoryService _categoryService;

    public EditModel(
        IMovieService movieService,
        IGenreService genreService,
        IMovieGenreService movieGenreService,
        ICategoryService categoryService)
    {
        _movieService = movieService;
        _genreService = genreService;
        _movieGenreService = movieGenreService;
        _categoryService = categoryService;
    }

    // 🟡 Dùng trực tiếp BindProperty thay vì DTO
    [BindProperty] public Movie Movie { get; set; } = new();
    [BindProperty] public MovieDetail Detail { get; set; } = new();
    [BindProperty] public List<int> SelectedGenres { get; set; } = new();

    public List<SelectListItem> GenreList { get; set; } = new();
    public List<SelectListItem> CategoryList { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var movie = await _movieService.GetWithDetailsAsync(id);
        if (movie == null) return NotFound();

        Movie = movie;
        Detail = movie.MovieDetails.FirstOrDefault() ?? new MovieDetail();
        SelectedGenres = (await _movieGenreService.GetGenreIdsByMovieIdAsync(movie.MovieId)).ToList();

        var allGenres = await _genreService.GetAllAsync();
        GenreList = allGenres.Select(g => new SelectListItem
        {
            Text = g.GenreDesc,
            Value = g.GenreId.ToString(),
            Selected = SelectedGenres.Contains(g.GenreId)
        }).ToList();

        var allCategories = await _categoryService.GetAllAsync();
        CategoryList = allCategories.Select(c => new SelectListItem
        {
            Text = c.CategoryDesc,
            Value = c.CategoryId.ToString(),
            Selected = (c.CategoryId == Movie.CategoryId)
        }).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _movieService.UpdateWithRelationsAsync(Movie, Detail, SelectedGenres);
        return RedirectToPage("Index");
    }
}