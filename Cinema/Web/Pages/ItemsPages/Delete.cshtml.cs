using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly IItemService _itemService;

        public DeleteModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        [BindProperty]
        public ItemDto Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _itemService.GetByIdAsync(id);
            if (Item == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _itemService.DeleteAsync(Item.ItemId);
            return RedirectToPage("Index");
        }
    }
}
