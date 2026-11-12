using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data;

public  class MyDBContext : DbContext
{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
    {
        
    }

    // db table presentation 
    public DbSet<User> users {get;set;}
    public DbSet<Employee> employees {get;set;}
}