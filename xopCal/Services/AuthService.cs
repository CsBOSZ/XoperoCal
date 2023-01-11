using System.Buffers.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using xopCal.Entity;
using xopCal.Exceptions;
using xopCal.Model;

namespace xopCal.Services;

public class AuthService : IAuthService
{
    
    AuthenticationSettings authSetting = new AuthenticationSettings()
    {
    
        JwtKey= "PRIVATE_KEY_DONT_SHARE",
        JwtExpireDays= 15,
        JwtIssuer= "http://eventcalapi.com"
    
    };

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

    public string GetJwt(LoginDto dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

        if (user is null)
        {
            // throw new BadRequestException("Invalid email or password");
            return "error";
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        
        if (result == PasswordVerificationResult.Failed)
        {
            // throw new BadRequestException("Invalid email or password");
            return "error";
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(authSetting.JwtExpireDays);

        var token = new JwtSecurityToken(authSetting.JwtIssuer, authSetting.JwtIssuer, claims, expires: expires , signingCredentials:cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);

    }
}