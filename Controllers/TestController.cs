namespace CSharpService.Controllers;

using Microsoft.AspNetCore.Mvc;
using CSharpService.Services;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TestController(ITestService testService) : ControllerBase
{

    [HttpGet("void")]
    public IActionResult Void()
    {
        return Ok(new { id = 1 });
    }

    [HttpGet("calculation")]
    public IActionResult Calculation()
    {
        return Ok(new { total = testService.HeavyCalculation(1000000) });
    }
}