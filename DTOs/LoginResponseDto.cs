namespace EmployeeManagementSystem.DTOs;

public class LoginResponseDto
{
    public String? Token { get; set; }
    public int UserId { get; set; }
    public String? FullName { get; set; }
    public String? Email { get; set; }
    public String? Role { get; set; }
    public DateTime Expiration { get; set;}
}