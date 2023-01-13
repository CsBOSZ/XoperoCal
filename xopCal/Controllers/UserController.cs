using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using xopCal.Entity;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;

[Route("User")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet("{events}/my")]
    [Authorize]
    public ActionResult<UserDtoOut> GetMy([FromRoute]bool events)
    {
        UserDtoOut? u = null;
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            u = _userService.GetUserById(result,events);
        }
        return u is null ? NotFound() : Ok(u);
    }
    
    [HttpGet("{events}/{id}")]
    public ActionResult<UserDtoOut> GetUserById([FromRoute]bool events,[FromRoute]int id)
    {
        var u = _userService.GetUserById(id,events);
        return u is null ? NotFound() : Ok(u);
    }

    [HttpGet("{events}/name/{name}")]
    public ActionResult<List<UserDtoOut>> GetUserByName([FromRoute]bool events,[FromRoute]string name)
    {
        var u = _userService.GetAllUserByName(name,events);
        return u.IsNullOrEmpty() ? NotFound() : Ok(u);
    }
  
    [HttpGet("{events}/email/{email}")]
    public ActionResult<UserDtoOut> GetUserByEmail([FromRoute]bool events,[FromRoute]string email)
    {
        var u = _userService.GetUserByEmail(email,events);
        return u is null ? NotFound() : Ok(u);
    }
    
    [HttpGet("{events}/all")]
    public ActionResult<List<UserDtoOut>> GetAll([FromRoute]bool events)
    {
        var u = _userService.GetAll(events);
        return u.IsNullOrEmpty() ? NotFound() : Ok(u);
    }
    
    
    
}