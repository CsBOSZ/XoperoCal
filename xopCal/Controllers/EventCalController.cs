using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using xopCal.Entity;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;

[EnableCors("MyPolicy")]
[Route("Event")]
[ApiController]
[Authorize]
public class EventCalController : ControllerBase
{

    private readonly IEventCalService _eventCalService;
    
    // public ActionResult
    public EventCalController(IEventCalService eventCalService)
    {
        _eventCalService = eventCalService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<EventCalDtoOut> GetEventCalById([FromRoute]int id)
    {
        var e = _eventCalService.GetEventCalById(id);
        return e is null ? NotFound() : Ok(e);
    }
    
    [HttpGet("my")]
    public ActionResult<List<EventCalDtoOut>> GetAllEventCalByUserId()
    {
        var le = new List<EventCalDtoOut>(); 
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            le = _eventCalService.GetAllEventCalByUserId(result);
        }
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpGet("all")]
    [AllowAnonymous]
    public ActionResult<List<EventCalDtoOut>> GetAll()
    {
        var le = _eventCalService.GetAll();
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpGet("userId/{id}")]
    [AllowAnonymous]
    public ActionResult<List<EventCalDtoOut>> GetAllEventCalByUserId(int id)
    {
        var le = _eventCalService.GetAllEventCalByUserId(id);
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }

    [HttpGet("time")]
    [AllowAnonymous]
    public ActionResult<List<EventCalDtoOut>> GetAllEventCalByTime([FromBody]TimeDto dto)
    {
        var le = _eventCalService.GetAllEventCalByTime(dto);
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpGet("eventName/{name}")]
    [AllowAnonymous]
    public ActionResult<List<EventCalDtoOut>> GetAllEventCalByName([FromRoute]string name)
    {
        var le = _eventCalService.GetAllEventCalByName(name);
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpPost]
    public ActionResult PostEventCal([FromBody]EventCalDtoIn dtoIn)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.PostEventCal(dtoIn, result) ? Ok() : BadRequest();
        }
        return BadRequest();
    }
  
    [HttpPut]
    public ActionResult PutEventCal([FromBody]EventCalDtoIn dtoIn)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.PutEventCal(dtoIn, result) ? Ok() : BadRequest();
        }
        return BadRequest();
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteEventCal([FromRoute]int id)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.DeleteEventCal(id, result) ? Ok() : BadRequest();
        }
        return BadRequest();
    }
    
    [HttpPut("Subscribe/{id}")]
    public ActionResult Subscribe([FromRoute]int id)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.Subscribe(id, result) ? Ok() : BadRequest();
        }
        return BadRequest();
    }
    
}