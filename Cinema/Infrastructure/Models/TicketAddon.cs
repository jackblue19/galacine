using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class TicketAddon
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int ServiceId { get; set; }

    public int ItemId { get; set; }

    public int? Quantity { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
