using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Data.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class MovieGenreService : IMovieGenreService
{
    private readonly CinemaDbContext _context;

    public MovieGenreService(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<int>> GetGenreIdsByMovieIdAsync(int movieId)
    {
        return await _context.Set<MovieGenre>()
            .Where(mg => mg.MovieId == movieId)
            .Select(mg => mg.GenreId)
            .ToListAsync();
    }
}

