using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceDesc { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int? ApprovedBy { get; set; }

    public bool? IsApproved { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<TicketAddon> TicketAddons { get; set; } = new List<TicketAddon>();
}
