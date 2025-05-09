﻿using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class MovieBasePrice
{
    public int MovieId { get; set; }

    public string SeatType { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
