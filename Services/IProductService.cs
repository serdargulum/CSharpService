namespace CSharpService.Services;

public interface IProductService
{
    public Task<List<Product>> GetProductsAsync();
}