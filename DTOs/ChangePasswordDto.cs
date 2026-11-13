using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.DTOs;

public class ChangePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword", ErrorMessage = "New password and confirm password do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}