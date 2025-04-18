using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int ItemId { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int CurrentStock { get; set; }

    public int? MinStockLevel { get; set; }

    public virtual Item Item { get; set; } = null!;
}
