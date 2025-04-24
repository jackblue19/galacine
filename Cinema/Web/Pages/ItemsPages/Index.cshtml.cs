using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IItemService _itemService;
        public List<ItemDto> Items { get; set; }

        public IndexModel(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task OnGetAsync()
        {
            Items = await _itemService.GetAllAsync();
        }
    }
}
