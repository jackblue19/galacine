using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class MovieDetail
{
    public int MovieDetailId { get; set; }

    public int MovieId { get; set; }

    public string? Director { get; set; }

    public string? Actors { get; set; }

    public string? AgeLimit { get; set; }

    public string? Language { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
