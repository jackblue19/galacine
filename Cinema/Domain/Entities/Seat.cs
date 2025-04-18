using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Seat
{
    public int SeatId { get; set; }

    public int RoomId { get; set; }

    public int RowNo { get; set; }

    public int ColNo { get; set; }

    public string? SeatType { get; set; }

    public string? SeatStatus { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
