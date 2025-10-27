namespace CSharpService.Models;

using Microsoft.EntityFrameworkCore;

public class Product
{
    public long Id { get; set; }
    public long? BrandId { get; set; }
    public long? ProductCategoryId { get; set; }
    public long? TaxId { get; set; }
    public int StockType { get; set; } = 0;
    public ushort Stock { get; set; } = 0;
    public string CurrencyCode { get; set; } = "USD";
    [Precision(18, 2)]
    public decimal Price { get; set; } = 0.00M;
    public bool IsPrepackedItem { get; set; } = false;
    public bool IsActive { get; set; } = false;
    [Precision(18, 2)]
    public decimal PackagingPrice { get; set; } = 0.00M;
    public ushort? Calories { get; set; }
    public string? SKU { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
