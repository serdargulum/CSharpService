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
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await productService.GetProductsAsync());
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(productService.GetProducts());
    }

    [HttpGet("sql")]
    public async Task<IActionResult> GetBySql(IConfiguration configuration)
    {
        return Ok(await productService.GetProductsSql(configuration));
    }

    [HttpGet("dapper")]
    public async Task<IActionResult> GetDapper(IConfiguration configuration)
    {
        return Ok(await productService.GetProductsDapperAsync(configuration));
    }

}
