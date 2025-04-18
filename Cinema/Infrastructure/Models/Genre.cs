﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreDesc { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
