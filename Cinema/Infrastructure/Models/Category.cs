using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryDesc { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
