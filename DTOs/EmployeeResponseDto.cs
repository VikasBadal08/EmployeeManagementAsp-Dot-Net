
namespace EmployeeManagementSystem.DTOs;

public class EmployeeResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string EmployeeCode { get; set; } = string.Empty;

    public string Designation { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public decimal Salary { get; set; }

    public DateTime JoiningDate { get; set; }

    public decimal ExperienceYears { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string EmployeeType { get; set; } = string.Empty;

    public string? ManagerName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
