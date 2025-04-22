using Data.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.Admin.Schedules
{
    public class IndexModel : PageModel
    {
        private readonly ISchedulesService _schedulesService;

        public IndexModel(ISchedulesService schedulesService)
        {
            _schedulesService = schedulesService;
        }

        public IList<Schedule> Schedules { get; set; } = new List<Schedule>();

        public async Task OnGetAsync()
        {
            Schedules = (await _schedulesService.GetAll()).ToList();

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var deleted = await _schedulesService.DeleteAsync(id);
                if (!deleted)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch chiếu để xóa.";
                }
                else
                {
                    TempData["SuccessMessage"] = "Xóa lịch chiếu thành công.";
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message; // "Không thể xóa lịch chiếu của ngày hiện tại."
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi xóa lịch chiếu: " + ex.Message;
            }

            return RedirectToPage();
        }
    }
}
