using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Interfaces;

public interface IEmployeeRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> EmployeeCodeExistsAsync(string employeecode);
    Task<Employee> CreateEmployeeWithUserAsync(User user, Employee employee);

    Task<List<Employee>> GetAllEmployeeAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<bool> EmailExistsForOtherUserAsync(string email, int userId);
    Task<bool> EmployeeCodeExistsForOtherEmployeeAsync(string employeeCode, int employeeId);

    Task UpdateEmployeeAsync(Employee employee);

    Task<bool> DeactivateEmployeeAsync(int id);
    Task<bool> PermanentDeleteEmployeeAsync(int id);

    Task<bool> ActivateEmployeeAsync(int id);


    // employee api ke liye
    Task<Employee?> GetEmployeeByUserIdAsync(int UaserId);

    Task UpdateMyProfileAsync(Employee employee);

    Task<User?> GetUserByIdAsync(int userId);

    Task UpdateUserPasswordAsync(User user);

}