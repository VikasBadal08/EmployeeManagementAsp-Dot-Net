using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.DTOs;

namespace EmployeeManagementSystem.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAdminAsync(
        RegisterRequestDto request);

    Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto request);
}