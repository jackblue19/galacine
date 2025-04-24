using Application.DTOs;
using Application.Interfaces.Repositories;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Vouchers
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public CreateModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        [BindProperty]
        public CreateVoucherDto Voucher { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entity = new Voucher
            {
                Code = Voucher.Code,
                VoucherDesc = Voucher.VoucherDesc,
                DiscountPercent = Voucher.DiscountPercent,
                ExpiredDate = DateOnly.FromDateTime(Voucher.ExpiredDate),
                IsActive = Voucher.IsActive,
                MinPurchaseAmount = Voucher.MinPurchaseAmount
            };

            await _voucherRepository.AddAsync(entity);
            return RedirectToPage("Index");
        }
    }
}
