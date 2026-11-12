
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOs;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Employee code is required")]
    [StringLength(20)]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required")]
    [StringLength(100)]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "9999999999999999.99")]
    public decimal Salary { get; set; }

    [Required]
    public DateTime JoiningDate { get; set; }

    [Range(0, 60)]
    public decimal ExperienceYears { get; set; }

    [Phone]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [Required]
    [RegularExpression("^(Full-time|Part-time|Intern|Contract)$")]
    public string EmployeeType { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ManagerName { get; set; }
}
