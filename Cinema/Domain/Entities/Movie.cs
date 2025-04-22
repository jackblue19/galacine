using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Movie
{
    public int MovieId { get; set; }

    public string MovieName { get; set; } = null!;

    public string? MovieDesc { get; set; }

    public string? MovieImg { get; set; }

    public string MovieStatus { get; set; } = null!;

    public string? Trailer { get; set; }

    public double? Rating { get; set; }

    public string? Nation { get; set; }

    public bool? IsSub { get; set; }

    public int Duration { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Language { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<MovieBasePrice> MovieBasePrices { get; set; } = new List<MovieBasePrice>();

    public virtual ICollection<MovieDetail> MovieDetails { get; set; } = new List<MovieDetail>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    //public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();

}
