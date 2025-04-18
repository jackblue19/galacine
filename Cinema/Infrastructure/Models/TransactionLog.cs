using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class TransactionLog
{
    public int TransactionId { get; set; }

    public int BillId { get; set; }

    public string? PaymentGateway { get; set; }

    public string? TransactionCode { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Bill Bill { get; set; } = null!;
}
