using Microsoft.AspNetCore.Mvc;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;

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
    public ActionResult RegisterUser([FromBody]UserDto dto)
    {
        _authService.RegisterUser(dto);
        return Ok();
    }
    
    [HttpPost("login")]
    public ActionResult<string> Login([FromBody]LoginDto dto)
    {
        var token = _authService.GetJwt(dto);
        return Ok(token);
    }
}