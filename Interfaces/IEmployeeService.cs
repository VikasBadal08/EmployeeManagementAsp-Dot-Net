using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Interfaces;

public interface IEmployeeService
{
    Task<EmployeeResponseDto?> createEmployeeAsync(CreateEmployeeDto request);
    Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

    Task<(EmployeeResponseDto? Data, string? Error)> UpdateEmployeeAsync(
        int id, UpdateEmployeeDto request);

    Task<bool> DeactivateEmployeeAsync(int id);

    Task<bool> PermanentDeleteEmployeeAsync(int id);

    Task<bool> ActivateEmployeeAsync(int id);
}