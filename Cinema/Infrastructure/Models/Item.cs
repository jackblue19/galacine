using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string? ItemDesc { get; set; }

    public decimal? SuggestedPrice { get; set; }

    public decimal? OriginalPrice { get; set; }

    public string? Type { get; set; }

    public string? ItemCategory { get; set; }

    public int? Amount { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<TicketAddon> TicketAddons { get; set; } = new List<TicketAddon>();
}
