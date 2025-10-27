using CSharpService.Data;
using CSharpService.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CSharpService.Services;

public class ProductService(AppDbContext context) : IProductService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }
}
