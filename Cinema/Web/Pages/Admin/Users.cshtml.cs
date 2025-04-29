using Application.Services;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public UsersModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public List<User> Customers { get; set; }

        public async Task OnGetAsync()
        {
            Customers = await _dashboardService.GetAllCustomersAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int userId)
        {
            var success = await _dashboardService.DeleteCustomerAsync(userId);
            if (success)
            {
                TempData["SuccessMessage"] = "Customer deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Customer to delete movie.";
            }

            return RedirectToPage(); // reload page
        }
    
}

}
