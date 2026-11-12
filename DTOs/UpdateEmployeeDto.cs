using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOs;

public class UpdateEmployeeDto
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Designation { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal Salary { get; set; }

    public DateTime JoiningDate { get; set; }

    [Range(0, 60)]
    public decimal ExperienceYears { get; set; }

    [Phone]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [Required]
    [RegularExpression("Full-time|Part-time|Intern|Contract")]
    public string EmployeeType { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ManagerName { get; set; }
}