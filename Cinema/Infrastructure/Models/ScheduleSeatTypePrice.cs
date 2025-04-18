using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ScheduleSeatTypePrice
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public string? SeatType { get; set; }

    public decimal Price { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}
