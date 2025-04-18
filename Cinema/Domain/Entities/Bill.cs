using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Bill
{
    public int BillId { get; set; }

    public string? BillStatus { get; set; }

    public decimal TotalCost { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? FinalCost { get; set; }

    public DateTime? BillDateTime { get; set; }

    public string? BillType { get; set; }

    public string? PaymentMethod { get; set; }

    public bool? IsPaid { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public int? VoucherId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual ICollection<TransactionLog> TransactionLogs { get; set; } = new List<TransactionLog>();

    public virtual User User { get; set; } = null!;

    public virtual Voucher? Voucher { get; set; }
}
