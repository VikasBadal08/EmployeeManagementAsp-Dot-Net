using System.Transactions;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repositories;

public class EmployeeRespository : IEmployeeRepository
{
    private readonly MyDBContext context;

    public EmployeeRespository(MyDBContext context)
    {
        this.context = context;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await context.users.AnyAsync(u => u.Email == email);
    }
    public async Task<bool> EmployeeCodeExistsAsync(string employeecode)
    {
        return await context.employees.AnyAsync(u => u.EmployeeCode == employeecode);
    }

    public async Task<Employee> CreateEmployeeWithUserAsync(User user, Employee employee)
    {
        await using var Transaction = await context.Database.BeginTransactionAsync();
        try
        {
            await context.users.AddAsync(user);
            await context.SaveChangesAsync();

            employee.UserId = user.Id;

            await context.employees.AddAsync(employee);
            await context.SaveChangesAsync();

            await Transaction.CommitAsync();

            return employee;
        }
        catch
        {
            await Transaction.RollbackAsync();
            throw;
        }
    }


    public async Task<List<Employee>> GetAllEmployeeAsync()
    {
        return await context.employees
            .Include(e => e.User)
            .Where(e => e.User != null && e.User.IsActive)
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id)
    {
        return await context.employees.Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id && e.User != null && e.User.IsActive);
    }


    public async Task<bool> EmailExistsForOtherUserAsync(string email, int userId)
    {
        return await context.users
            .AnyAsync(u => u.Email == email && u.Id != userId);
    }

    public async Task<bool> EmployeeCodeExistsForOtherEmployeeAsync(
    string employeeCode, int employeeId)
    {
        return await context.employees
            .AnyAsync(e => e.EmployeeCode == employeeCode && e.Id != employeeId);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeactivateEmployeeAsync(int id)
    {
        var employee = await context.employees
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id && e.User != null);

        if (employee == null || employee.User == null)
            return false;

        employee.User.IsActive = false;

        await context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> ActivateEmployeeAsync(int id)
    {
        var employee = await context.employees
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null || employee.User == null)
            return false;

        employee.User.IsActive = true;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> PermanentDeleteEmployeeAsync(int id)
    {
        var employee = await context.employees
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (employee == null || employee.User == null)
            return false;

        await using var transaction =
            await context.Database.BeginTransactionAsync();

        try
        {
            // First delete Employee
            context.employees.Remove(employee);

            // Then delete User
            context.users.Remove(employee.User);

            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}