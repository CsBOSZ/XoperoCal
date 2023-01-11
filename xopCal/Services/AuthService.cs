using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;

public class AuthService : IAuthService
{

    private readonly EventDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(EventDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public void RegisterUser(UserDto dto)
    {
        var user = new User()
        {
            
            Name = dto.Name,
            Email = dto.Email,

        };
        var hasedPasword = _passwordHasher.HashPassword(user, dto.Password);
        user.PasswordHash = hasedPasword;
        _context.Users.Add(user);
        _context.SaveChanges();

    }
    
    
    
}