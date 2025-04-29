using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    public class ItemsModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public ItemsModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<dynamic> Items { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _dashboardService.GetAllItemsSoldAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int itemId)
        {
            var success = await _dashboardService.DeleteItemAsync(itemId);
            if (success)
            {
                TempData["SuccessMessage"] = "Items deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Items to delete movie.";
            }

            return RedirectToPage(); // reload page
        }
    }

}
