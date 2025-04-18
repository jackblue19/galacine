using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class AuditLog
{
    public int LogId { get; set; }

    public string? Action { get; set; }

    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Description { get; set; }

    public virtual User? User { get; set; }
}
