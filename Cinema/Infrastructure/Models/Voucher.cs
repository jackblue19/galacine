using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string Code { get; set; } = null!;

    public string? VoucherDesc { get; set; }

    public decimal? DiscountPercent { get; set; }

    public DateOnly ExpiredDate { get; set; }

    public bool? IsActive { get; set; }

    public decimal? MinPurchaseAmount { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
