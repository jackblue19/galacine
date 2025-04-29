using Application.Interfaces.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Manager.MovieManager;

public class UpdateModel : PageModel
{
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    private readonly ICategoryService _categoryService;
    private readonly IMovieGenreService _movieGenreService;
    private readonly IMovieDetailService _movieDetailService;

    public UpdateModel(IMovieService movieService,
                       IGenreService genreService,
                       ICategoryService categoryService,
                       IMovieDetailService movieDetailService,
                       IMovieGenreService movieGenreService)
    {
        _movieService = movieService;
        _genreService = genreService;
        _categoryService = categoryService;
        _movieDetailService = movieDetailService;
        _movieGenreService = movieGenreService;
    }

    [BindProperty] public Movie Movie { get; set; } = new();
    [BindProperty] public MovieDetail Detail { get; set; } = new();
    [BindProperty] public List<int> SelectedGenres { get; set; } = new();
    [BindProperty] public Dictionary<string, decimal> BasePrices { get; set; } = new();  // Optional nếu bạn thêm MovieBasePrice

    public List<SelectListItem> GenreList { get; set; } = new();
    public List<SelectListItem> CategoryList { get; set; } = new();
    [BindProperty] public bool IsEditing { get; set; } = false;
    [BindProperty] public string? AgeLimit { get; set; }
    [BindProperty] public bool IsSubBool { get; set; }


    public async Task<IActionResult> OnGetAsync(int id)
    {
        // 1. Load Movie
        Movie = await _movieService.GetByIdAsync(id);
        if (Movie == null) return NotFound();

        // 2. Load Detail
        Detail = await _movieDetailService.GetByMovieAsync(id) ?? new MovieDetail { MovieId = id };

        // 3. Load Selected Genres
        SelectedGenres = (await _movieGenreService.GetGenreIdsByMovieIdAsync(id)).ToList();

        // 4. Load Genres for Display
        GenreList = (await _genreService.GetAllAsync())
            .Select(g => new SelectListItem
            {
                Text = g.GenreDesc,
                Value = g.GenreId.ToString(),
                Selected = SelectedGenres.Contains(g.GenreId)
            })
            .ToList();

        // 5. Load Categories
        CategoryList = (await _categoryService.GetAllAsync())
            .Select(c => new SelectListItem
            {
                Text = c.CategoryDesc,
                Value = c.CategoryId.ToString()
            })
            .ToList();

        AgeLimit = Detail.AgeLimit ?? "";
        IsSubBool = Movie.IsSub ?? false;

        // 6. (Optional) Load BasePrice
        //BasePrices = await _movieService.GetBasePricesByMovieIdAsync(id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        //if (!ModelState.IsValid) return Page();

        Detail.AgeLimit = AgeLimit;
        Movie.IsSub = IsSubBool;

        // Cập nhật Movie + MovieDetail + Genres
        //await _movieService.UpdateWithRelationsAsync(Movie, Detail, SelectedGenres, BasePrices);
        await _movieService.UpdateWithRelationsAsync(Movie, Detail, SelectedGenres);

        return RedirectToPage("Index");
    }
}
