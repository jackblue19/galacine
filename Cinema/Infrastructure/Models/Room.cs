using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int Capacity { get; set; }

    public string RoomName { get; set; } = null!;

    public string? RoomType { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
