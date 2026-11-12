using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystem.helpers;
public class Jwthelper
{
    private readonly IConfiguration configuration;

    public Jwthelper(IConfiguration configuration)
    {
        this.configuration=configuration;
    }

    public string CreateToken(User user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

        var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        var claims = new []
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),            
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),            
            new Claim(ClaimTypes.Name,user.FullName),            
            new Claim(ClaimTypes.Role,user.Role),            
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["DurationInMinutes"]!)
            ),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}