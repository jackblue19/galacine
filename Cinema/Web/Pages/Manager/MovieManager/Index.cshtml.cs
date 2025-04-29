using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces.Services;
using Data.Entities;

namespace Web.Pages.Manager.MovieManager
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;

        public IndexModel(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public List<Movie> Movies { get; set; } = new();

        [BindProperty(SupportsGet = true)] public string? SearchQuery { get; set; }
        [BindProperty(SupportsGet = true)] public string SortBy { get; set; } = "MovieName";
        [BindProperty(SupportsGet = true)] public bool SortAsc { get; set; } = true;
        [BindProperty(SupportsGet = true)] public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 100;

        public int TotalPages => (int)Math.Ceiling(FilteredMovies.Count() / (double)PageSize);

        public IEnumerable<Movie> FilteredMovies { get; set; } = new List<Movie>();
        public IEnumerable<Movie> PagedMovies => FilteredMovies.Skip((Page - 1) * PageSize).Take(PageSize);

        public async Task OnGetAsync()
        {
            var allMovies = await _movieService.GetAllAsync();

            // Search
            var query = allMovies.AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                query = query.Where(m => m.MovieName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            query = SortBy switch
            {
                "Duration" => SortAsc ? query.OrderBy(m => m.Duration) : query.OrderByDescending(m => m.Duration),
                "MovieName" => SortAsc ? query.OrderBy(m => m.MovieName) : query.OrderByDescending(m => m.MovieName),
                _ => query
            };

            FilteredMovies = query.ToList();
            Movies = FilteredMovies.ToList();
        }
    }
}
