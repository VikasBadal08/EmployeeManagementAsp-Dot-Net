
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Employee code is required")]
    [StringLength(20, ErrorMessage = "Employee code cannot exceed 20 characters")]
    public string EmployeeCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Designation is required")]
    [StringLength(100)]
    public string Designation { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required(ErrorMessage = "Salary is required")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 9999999999999999.99,
        ErrorMessage = "Salary must be greater than zero")]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = "Joining date is required")]
    public DateTime JoiningDate { get; set; }

    [Range(0, 60, ErrorMessage = "Experience must be between 0 and 60 years")]
    [Column(TypeName = "decimal(4,2)")]
    public decimal ExperienceYears { get; set; }

    [Phone(ErrorMessage = "Enter a valid phone number")]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Employment type is required")]
    [RegularExpression("^(Full-time|Part-time|Intern|Contract)$",
        ErrorMessage = "Invalid employment type")]
    public string EmployeeType { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ManagerName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation Property
    public User? User { get; set; }
}
