namespace CSharpService.Services;

using CSharpService.Models;

public interface IProductService
{
    public Task<List<Product>> GetProductsAsync();

    public List<Product> GetProducts();

    public Task<List<Product>> GetProductsSql(IConfiguration configuration);

    public Task<List<Product>> GetProductsDapperAsync(IConfiguration configuration);
}