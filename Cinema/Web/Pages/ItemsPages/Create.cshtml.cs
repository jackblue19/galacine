using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.DTOs;
using Application.Interfaces.Services;

namespace Web.Pages.ItemsPages
{
    public class CreateModel : PageModel
    {
        private readonly IItemService _itemService;

        public CreateModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        [BindProperty]
        public CreateItemDto Item { get; set; }

        public SelectList TypeOptions { get; set; }
        public SelectList CategoryOptions { get; set; }

        public void OnGet()
        {
            LoadDropdowns();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();
                return Page();
            }

            await _itemService.CreateAsync(Item);
            return RedirectToPage("Index");
        }

        private void LoadDropdowns()
        {
            TypeOptions = new SelectList(new[] { "Snack", "Beverage", "Comfort" });
            CategoryOptions = new SelectList(new[] { "Food", "Drink", "Service" });
        }
    }
}
