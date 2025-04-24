using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VoucherDto
    {
        public int VoucherId { get; set; }
        public string Code { get; set; } = null!;
        public string? VoucherDesc { get; set; }
        public decimal? DiscountPercent { get; set; }
        public DateOnly ExpiredDate { get; set; }
        public bool IsActive { get; set; }
        public decimal? MinPurchaseAmount { get; set; }
    }

    public class CreateVoucherDto
    {
        public string Code { get; set; } = null!;
        public string? VoucherDesc { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsActive { get; set; }
        public decimal MinPurchaseAmount { get; set; }
    }

    public class UpdateVoucherDto
    {
        public string? VoucherDesc { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsActive { get; set; }
        public decimal MinPurchaseAmount { get; set; }
    }
}
