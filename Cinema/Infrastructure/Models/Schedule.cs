using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public DateTime StartDatetime { get; set; }

    public DateTime EndDatetime { get; set; }

    public bool? Is3D { get; set; }

    public bool? IsSubtitle { get; set; }

    public int MovieId { get; set; }

    public int RoomId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<ScheduleSeatTypePrice> ScheduleSeatTypePrices { get; set; } = new List<ScheduleSeatTypePrice>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
