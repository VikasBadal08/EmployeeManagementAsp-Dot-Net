using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IAuthService authservice;

    public AuthController(IAuthService authservice)
    {
        this.authservice = authservice;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register (RegisterRequestDto request)
    {
        var result = await authservice.RegisterAdminAsync(request);

        if (!result.Success)
        {
            return Conflict(result);
        }

        return Ok(result);

    } 
    [HttpPost("login")]
    public async Task<IActionResult> Login (LoginRequestDto request)
    {
        var result = await authservice.LoginAsync(request);

        if (result==null)
        {
            return Unauthorized(new
            {
                message="Invalid email or password."
            });
        }

        return Ok(result);

    } 
}