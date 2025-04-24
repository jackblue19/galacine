using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.DTOs;
using Application.Interfaces.Services;

namespace Web.Pages.ItemsPages;

public class EditModel : PageModel

{
    private readonly IItemService _itemService;

    public EditModel(IItemService itemService)
    {
        _itemService = itemService;
    }

    [BindProperty]
    public UpdateItemDto Item { get; set; }

    public SelectList TypeOptions { get; set; }
    public SelectList CategoryOptions { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var itemDto = await _itemService.GetByIdAsync(id);
        if (itemDto == null) return NotFound();

        // Map ItemDto to UpdateItemDto
        Item = new UpdateItemDto
        {
            ItemDesc = itemDto.ItemDesc,
            SuggestedPrice = itemDto.SuggestedPrice ?? 0,
            OriginalPrice = itemDto.OriginalPrice ?? 0,
            Type = itemDto.Type,
            ItemCategory = itemDto.ItemCategory,
            Amount = itemDto.Amount ?? 0
        };

        LoadDropdowns();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns();
            return Page();
        }

        await _itemService.UpdateAsync(id, Item);
        return RedirectToPage("Index");
    }

    private void LoadDropdowns()
    {
        // Based on your DB screenshot:
        TypeOptions = new SelectList(new[] { "Snack", "Beverage", "Comfort" });
        CategoryOptions = new SelectList(new[] { "Food", "Drink", "Service" });
    }
}
