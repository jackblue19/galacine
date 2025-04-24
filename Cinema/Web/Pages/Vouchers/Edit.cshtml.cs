using Application.DTOs;
using Application.Interfaces.Repositories;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Vouchers
{
    public class EditModel : PageModel
    {
        private readonly IVoucherRepository _voucherRepository;

        public EditModel(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        [BindProperty]
        public CreateVoucherDto Voucher { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var entity = await _voucherRepository.GetByIdAsync(Id);
            if (entity == null)
            {
                return NotFound();
            }

            Voucher = new CreateVoucherDto
            {
                Code = entity.Code,
                VoucherDesc = entity.VoucherDesc,
                DiscountPercent = entity.DiscountPercent ?? 0,
                MinPurchaseAmount = entity.MinPurchaseAmount ?? 0,
                ExpiredDate = entity.ExpiredDate.ToDateTime(TimeOnly.MinValue),
                IsActive = entity.IsActive ?? false
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entity = await _voucherRepository.GetByIdAsync(Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Code = Voucher.Code;
            entity.VoucherDesc = Voucher.VoucherDesc;
            entity.DiscountPercent = Voucher.DiscountPercent;
            entity.MinPurchaseAmount = Voucher.MinPurchaseAmount;
            entity.ExpiredDate = DateOnly.FromDateTime(Voucher.ExpiredDate);
            entity.IsActive = Voucher.IsActive;

            await _voucherRepository.UpdateAsync(entity);
            return RedirectToPage("Index");
        }
    }
}
