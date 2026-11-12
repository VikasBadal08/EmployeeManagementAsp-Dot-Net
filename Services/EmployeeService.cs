using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository repository;
    private readonly IPasswordHasher<User> passwordHasher;

    public EmployeeService(IEmployeeRepository repository, IPasswordHasher<User> passwordHasher)
    {
        this.repository = repository;
        this.passwordHasher = passwordHasher;
    }

    public async Task<EmployeeResponseDto?> createEmployeeAsync(CreateEmployeeDto request)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        if (await repository.EmailExistsAsync(email))
        {
            return null;
        }
        if (await repository.EmployeeCodeExistsAsync(request.EmployeeCode.Trim()))
        {
            return null;
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = "Employee",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        var employee = new Employee
        {
            EmployeeCode = request.EmployeeCode.Trim(),
            Designation = request.Designation.Trim(),
            Department = request.Department.Trim(),
            Salary = request.Salary,
            JoiningDate = request.JoiningDate,
            ExperienceYears = request.ExperienceYears,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            EmployeeType = request.EmployeeType,
            ManagerName = request.ManagerName,
            CreatedAt = DateTime.UtcNow
        };

        await repository.CreateEmployeeWithUserAsync(user, employee);
        return new EmployeeResponseDto
        {
            Id = employee.Id,
            UserId = employee.UserId,
            FullName = user.FullName,
            Email = user.Email,
            EmployeeCode = employee.EmployeeCode,
            Designation = employee.Designation,
            Department = employee.Department,
            Salary = employee.Salary,
            JoiningDate = employee.JoiningDate,
            ExperienceYears = employee.ExperienceYears,
            PhoneNumber = employee.PhoneNumber,
            Address = employee.Address,
            EmployeeType = employee.EmployeeType,
            ManagerName = employee.ManagerName,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
            IsActive = user.IsActive
        };
    }

    public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
    {
        var employees = await repository.GetAllEmployeeAsync();

        return employees.Select(e => new EmployeeResponseDto
        {
            Id = e.Id,
            UserId = e.UserId,
            FullName = e.User!.FullName,
            Email = e.User.Email,
            EmployeeCode = e.EmployeeCode,
            Designation = e.Designation,
            Department = e.Department,
            Salary = e.Salary,
            JoiningDate = e.JoiningDate,
            ExperienceYears = e.ExperienceYears,
            PhoneNumber = e.PhoneNumber,
            Address = e.Address,
            EmployeeType = e.EmployeeType,
            ManagerName = e.ManagerName,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            IsActive = e.User.IsActive
        }).ToList();
    }

    public async Task<(EmployeeResponseDto? Data, string? Error)>
    UpdateEmployeeAsync(int id, UpdateEmployeeDto request)
    {
        var employee = await repository.GetEmployeeByIdAsync(id);

        if (employee == null || employee.User == null)
            return (null, null);

        var email = request.Email.Trim().ToLowerInvariant();

        if (await repository.EmailExistsForOtherUserAsync(
            email, employee.UserId))
        {
            return (null, "Email already exists.");
        }

        if (await repository.EmployeeCodeExistsForOtherEmployeeAsync(
            request.EmployeeCode, id))
        {
            return (null, "Employee code already exists.");
        }


        employee.User.FullName = request.FullName.Trim();
        employee.User.Email = email;

        employee.EmployeeCode = request.EmployeeCode.Trim();
        employee.Designation = request.Designation.Trim();
        employee.Department = request.Department.Trim();
        employee.Salary = request.Salary;
        employee.JoiningDate = request.JoiningDate;
        employee.ExperienceYears = request.ExperienceYears;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Address = request.Address;
        employee.EmployeeType = request.EmployeeType;
        employee.ManagerName = request.ManagerName;
        employee.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateEmployeeAsync(employee);

        var response = new EmployeeResponseDto
        {
            Id = employee.Id,
            UserId = employee.UserId,
            FullName = employee.User.FullName,
            Email = employee.User.Email,
            EmployeeCode = employee.EmployeeCode,
            Designation = employee.Designation,
            Department = employee.Department,
            Salary = employee.Salary,
            JoiningDate = employee.JoiningDate,
            ExperienceYears = employee.ExperienceYears,
            PhoneNumber = employee.PhoneNumber,
            Address = employee.Address,
            EmployeeType = employee.EmployeeType,
            ManagerName = employee.ManagerName,
            CreatedAt = employee.CreatedAt,
            UpdatedAt = employee.UpdatedAt,
            IsActive = employee.User.IsActive
        };

        return (response, null);
    }

    public async Task<bool> DeactivateEmployeeAsync(int id)
    {
        return await repository.DeactivateEmployeeAsync(id);
    }

    public async Task<bool> PermanentDeleteEmployeeAsync(int id)
    {
        return await repository.PermanentDeleteEmployeeAsync(id);
    }

    public async Task<bool> ActivateEmployeeAsync(int id)
    {
        return await repository.ActivateEmployeeAsync(id);
    }

}