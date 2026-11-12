using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Employee Management API is Working!");
    }

    [HttpGet("public")]
    public IActionResult PublicApi()
    {
        return Ok("This is a public API.");
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult ProtectedApi()
    {
        return Ok("You are successfully authenticated!");
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminApi()
    {
        return Ok("Welcome Admin! You can access this API.");
    }
}