using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IEmployeeService employeeService;

    public AdminController(IEmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEmployee(
        CreateEmployeeDto request)
    {
        var result = await employeeService.createEmployeeAsync(request);

        if (result == null)
        {
            return Conflict(new
            {
                message = "Email or Employee Code already exists."
            });
        }

        return Ok(result);
    }

    [HttpGet("Get-all")]
    public async Task<IActionResult> GetAllEmployee()
    {
        var employees = await employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeDto request)
    {
        var result = await employeeService.UpdateEmployeeAsync(id, request);
        if (result.Data == null && result.Error == null)
            return NotFound(new { message = "Employee not found." });

        if (result.Error != null)
            return Conflict(new { message = result.Error });

        return Ok(result.Data);
    }


    [HttpPatch("deactivate/{id:int}")]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        var result = await employeeService.DeactivateEmployeeAsync(id);

        if (!result)
            return NotFound(new { message = "Employee not found." });

        return Ok(new { message = "Employee deactivated successfully." });
    }

    [HttpPatch("{id:int}/activate")]
    public async Task<IActionResult> ActivateEmployee(int id)
    {
        var result = await employeeService.ActivateEmployeeAsync(id);

        if (!result)
            return NotFound(new { message = "Employee not found." });

        return Ok(new { message = "Employee activated successfully." });
    }

    [HttpDelete("{id:int}/permanent-Delete")]
    public async Task<IActionResult> PermanentDeleteEmployee(int id)
    {
        var result = await employeeService.PermanentDeleteEmployeeAsync(id);

        if (!result)
            return NotFound(new { message = "Employee not found." });

        return Ok(new { message = "Employee permanently deleted." });
    }




}