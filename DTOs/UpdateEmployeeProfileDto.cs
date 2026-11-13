using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOs;

public class UpdateEmployeeProfileDto
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Phone]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }
}