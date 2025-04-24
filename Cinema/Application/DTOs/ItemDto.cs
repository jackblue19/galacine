using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{ 
public class ItemDto
{
    public int ItemId { get; set; }
    public string? ItemDesc { get; set; }
    public decimal? SuggestedPrice { get; set; }
    public decimal? OriginalPrice { get; set; }
    public string? Type { get; set; }
    public string? ItemCategory { get; set; }
    public int? Amount { get; set; }
}

public class CreateItemDto
{
    public string? ItemDesc { get; set; }
    public decimal SuggestedPrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public string Type { get; set; }
    public string ItemCategory { get; set; }
    public int Amount { get; set; }
}
public class UpdateItemDto
{
    public string ItemDesc { get; set; } = string.Empty;
    public decimal SuggestedPrice { get; set; }
    public decimal OriginalPrice { get; set; }
    public string Type { get; set; } = string.Empty;
    public string ItemCategory { get; set; } = string.Empty;
    public int Amount { get; set; }
}
}

