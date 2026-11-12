using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.helpers;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository repository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly Jwthelper jwthelper;

    public AuthService(IAuthRepository repository, IPasswordHasher<User> passwordHasher, Jwthelper jwthelper)
    {
        this.repository = repository;
        this.passwordHasher = passwordHasher;
        this.jwthelper = jwthelper;
    }

    public async Task<RegisterResponseDto> RegisterAdminAsync(RegisterRequestDto request)
    {
        if (await repository.AdminExistsAsync())
        {
            return new RegisterResponseDto
            {
                Success = false,
                Message = "Admin registration is already completed."
            };
        }

        string email = request.Email.Trim().ToLowerInvariant();
        if (await repository.EmailExistsAsync(email))
        {
            return new RegisterResponseDto
            {
                Success = false,
                Message = "Email already exists."
            };
        }

        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = "Admin",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        user.PasswordHash = passwordHasher.HashPassword(user, request.Password);

        await repository.AddUserAsync(user);

        return new RegisterResponseDto
        {
            Success = true,
            Message = "Admin registered successfully.",
            UserId = user.Id
        };
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        string email = request.Email.Trim().ToLowerInvariant();
        var user = await repository.GetUserByEmailAsync(email);


        if (user == null || !user.IsActive)
        {
            return null;
        }

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var Token = jwthelper.CreateToken(user);

        return new LoginResponseDto
        {
            Token = Token,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            Role = user.Role!,
            Expiration = DateTime.UtcNow.AddMinutes(60)
        };
    }
}