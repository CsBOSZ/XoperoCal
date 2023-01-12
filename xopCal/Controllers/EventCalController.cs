using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using xopCal.Entity;
using xopCal.Model;
using xopCal.Services;

namespace xopCal.Controllers;


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
    public ActionResult<EventCal> GetEventCalById([FromRoute]int id)
    {
        var e = _eventCalService.GetEventCalById(id);
        return e is null ? NotFound() : Ok(e);
    }
    
    [HttpGet("my")]
    public ActionResult<List<EventCal>> GetAllEventCalByUserId()
    {
        var le = new List<EventCal>(); 
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            le = _eventCalService.GetAllEventCalByUserId(result);
        }
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpGet("time")]
    [AllowAnonymous]
    public ActionResult<List<EventCal>> GetAllEventCalByTime([FromBody]TimeDto dto)
    {
        var le = _eventCalService.GetAllEventCalByTime(dto);
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpGet("eventName/{name}")]
    [AllowAnonymous]
    public ActionResult<List<EventCal>> GetAllEventCalByName([FromRoute]string name)
    {
        var le = _eventCalService.GetAllEventCalByName(name);
        return le.IsNullOrEmpty() ? NotFound() : Ok(le);
    }
    
    [HttpPost]
    public ActionResult PostEventCal([FromBody]EventCalDto dto)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.PostEventCal(dto, result) ? Ok() : BadRequest();
        }
        return BadRequest();
    }
  
    [HttpPut]
    public ActionResult PutEventCal([FromBody]EventCalDto dto)
    {
        var c = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (c is not null && int.TryParse(c.Value,out int result))
        {
            return _eventCalService.PutEventCal(dto, result) ? Ok() : BadRequest();
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
    
}