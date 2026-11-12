using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly MyDBContext context;

    public AuthRepository(MyDBContext context)
    {
        this.context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await context.users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> AdminExistsAsync()
    {
        return await context.users
            .AnyAsync(u => u.Role == "Admin");
    }
    public async Task AddUserAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.users.FirstOrDefaultAsync(u => u.Email == email);
    }
}