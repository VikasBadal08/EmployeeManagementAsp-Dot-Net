using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Interfaces;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);

    Task<bool> AdminExistsAsync();

    Task AddUserAsync(User user);

    Task<User?> GetUserByEmailAsync(string email);
}