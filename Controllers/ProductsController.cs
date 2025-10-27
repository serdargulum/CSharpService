namespace CSharpService.Controllers;

using Microsoft.AspNetCore.Mvc;
using CSharpService.Services;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await productService.GetProductsAsync();
    }

}
