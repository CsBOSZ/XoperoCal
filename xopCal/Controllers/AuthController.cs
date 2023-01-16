using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;

[EnableCors("MyPolicy")]
[Route("Auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody]UserDtoIn dtoIn)
    {
        _authService.RegisterUser(dtoIn);
        return Ok();
    }
    
    [HttpPost("login")]
    public ActionResult<string> Login([FromBody]LoginDto dto)
    {
        var token = _authService.GetJwt(dto);
        return token.StartsWith("error") ? NotFound(token) : Ok(token);
    }
    
    [HttpGet("test")]
    [Authorize]
    public ActionResult<string> test()
    {
       var c =  HttpContext.User.FindFirst(ClaimTypes.Email);
       return Ok(c.Value);
    }
    
}