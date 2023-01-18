using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using xopCal.Hubs;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;

[EnableCors("MyPolicy")]
[Route("Auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IHubContext<EventHub> _hub;

    public AuthController(IAuthService authService, IHubContext<EventHub> hub)
    {
        _authService = authService;
        _hub = hub;
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
        _hub.Clients.All.SendAsync("test");
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