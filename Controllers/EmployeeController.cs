using System.Security.Claims;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Employee")]

public class EmployeeController : Controller
{
    private readonly IEmployeeService employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpGet("Employee-profile")]
    public async Task<IActionResult> GetMyProfile()
    {
        var useridvalue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine("Logged In User ID: " + useridvalue);

        if (!int.TryParse(useridvalue, out int userId))
            return Unauthorized();
        var profile = await employeeService.GetMyProfileAsync(userId);

        if (profile == null)
        {
            return NotFound(new { message = "Employee profile not found." });
        }

        return Ok(profile);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateMyProfile(
        UpdateEmployeeProfileDto request)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userId))
            return Unauthorized();

        var result = await employeeService.UpdateMyProfileAsync(userId, request);

        if (result == null)
            return NotFound(new { message = "Employee profile not found." });

        return Ok(result);
    }

    [HttpPatch("change-password")]
    public async Task<IActionResult> changePassword(ChangePasswordDto request)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out int userid))
        {
            return Unauthorized();
        }

        var result = await employeeService.ChangePasswordAsync(userid, request);

        if (result != null)
        {
            return BadRequest(new { message = result });
        }
        return Ok(new { message = "passward changes sucessfully" });

    }
}