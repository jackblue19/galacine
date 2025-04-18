using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string? TicketType { get; set; }

    public string? TicketStatus { get; set; }

    public string? Noting { get; set; }

    public DateTime? TicketDateTime { get; set; }

    public decimal TicketPrice { get; set; }

    public int SeatId { get; set; }

    public int ScheduleId { get; set; }

    public int BillId { get; set; }

    public int UserId { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Schedule Schedule { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;

    public virtual ICollection<TicketAddon> TicketAddons { get; set; } = new List<TicketAddon>();

    public virtual User User { get; set; } = null!;
}
