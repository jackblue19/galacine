using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace WebApp.Pages.Vouchers
{
    public class IndexModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public IndexModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public List<VoucherDto> Vouchers { get; set; } = new();

        public async Task OnGetAsync()
        {
            var all = await _voucherService.GetAllAsync();
            Vouchers = all.ToList();
        }
    }
}
