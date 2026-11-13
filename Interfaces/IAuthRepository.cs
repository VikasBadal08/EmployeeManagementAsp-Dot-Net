using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Interfaces;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);

    Task AddUserAsync(User user);

    Task<User?> GetUserByEmailAsync(string email);
}