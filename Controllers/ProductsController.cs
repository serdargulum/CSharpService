namespace CSharpService.Controllers;

using Microsoft.AspNetCore.Mvc;
using CSharpService.Services;
using System.Threading.Tasks;
using CSharpService.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("async")]
    public async Task<IEnumerable<Product>> GetAsync()
    {
        return await productService.GetProductsAsync();
    }

    [HttpGet]
    public List<Product> Get()
    {
        return productService.GetProducts();
    }

    [HttpGet("sql")]
    public async Task<List<Product>> GetBySql(IConfiguration configuration)
    {
        return await productService.GetProductsSql(configuration);
    }

    [HttpGet("dapper")]
    public async Task<List<Product>> GetDapper(IConfiguration configuration)
    {
        return await productService.GetProductsDapperAsync(configuration);
    }

}
