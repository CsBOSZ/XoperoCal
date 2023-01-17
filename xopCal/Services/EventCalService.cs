using AutoMapper;
using Microsoft.EntityFrameworkCore;
using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;


public class EventCalService : IEventCalService
{
    private readonly EventDbContext _context;
    private readonly IMapper _mapper;
    public EventCalService(EventDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public EventCalDtoOut? GetEventCalById(int? id)
    {
        if (id is null)
        {
            return null;
        }
        return _mapper.Map<EventCalDtoOut>(_context.EventCals.Include(e => e.Owner).Include(e => e.Subscribers).FirstOrDefault(e => e.Id == id));
    }
    
    public List<EventCalDtoOut> GetAllEventCalByUserId(int userId)
    {
        var le = _context.EventCals.Where(e => e.OwnerId == userId).Include(e => e.Owner).Include(e => e.Subscribers).ToList();
        return _mapper.Map<List<EventCalDtoOut>>(le);
    }
    
    public List<EventCalDtoOut> GetAllEventCalByTime(TimeDto dto)
    {
        List<EventCal> le;
        if (dto.EndEvent is null)
        { 
            le = _context.EventCals.Where(e => e.StartEvent <= dto.StartEvent && e.EndEvent >= dto.StartEvent).Include(e => e.Owner).Include(e => e.Subscribers).ToList();
        }
        else
        {
            le = _context.EventCals.Where(e => e.StartEvent >= dto.StartEvent && e.EndEvent <= dto.EndEvent).Include(e => e.Owner).Include(e => e.Subscribers).ToList();
        }
        return _mapper.Map<List<EventCalDtoOut>>(le);
    }

    public List<EventCalDtoOut> GetAll()
    {
        return _mapper.Map<List<EventCalDtoOut>>(_context.EventCals.Include(e=>e.Owner).Include(e => e.Subscribers).ToList());
    }

    public List<EventCalDtoOut> GetAllEventCalByName(string name)
    {
        List<EventCal> le = _context.EventCals.Include(e => e.Owner).Where(e => e.Name.Contains(name) || e.Owner.Name.Contains(name)).Include(e => e.Subscribers).ToList();
        return _mapper.Map<List<EventCalDtoOut>>(le);
    }
    
    public bool PostEventCal(EventCalDtoIn dtoIn,int userId)
    {
        var e = new EventCal()
        {
            Name = dtoIn.Name ?? "none",
            Description = dtoIn.Description ?? "none",
            StartEvent = dtoIn.StartEvent ?? DateTime.Now,
            Color = dtoIn.Color ?? "ffffff",
            OwnerId = userId,
        };
        e.EndEvent = dtoIn.EndEvent ?? e.StartEvent.AddDays(1);
        _context.EventCals.Add(e);
        _context.SaveChanges();
        return true;
    }
    
    public bool PutEventCal(EventCalDtoIn dtoIn,int userId)
    {
        EventCal? e = _context.EventCals.FirstOrDefault(e => e.Id == dtoIn.Id);
        if (e is not null && e.OwnerId == userId)
        {
            e.Name = dtoIn.Name ?? e.Name;
            e.Description = dtoIn.Description ?? e.Description;
            e.StartEvent = dtoIn.StartEvent ?? e.StartEvent;
            e.EndEvent = dtoIn.EndEvent ?? e.EndEvent;
            e.Color = dtoIn.Color ?? e.Color;
            _context.EventCals.Update(e);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool DeleteEventCal(int id,int userId)
    {
        var e = _context.EventCals.FirstOrDefault(e => e.Id == id);
        if (e is not null && e.OwnerId == userId)
        {
            _context.EventCals.Remove(e);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Subscribe(int id,int userId)
    {
        var e = _context.EventCals.Include(e => e.Subscribers).FirstOrDefault(e => e.Id == id);
        var u = _context.Users.FirstOrDefault(u => u.Id == userId);

        if (e is not null && u is not null && e.OwnerId != userId)
        {
            e.Subscribers.Add(u);
            _context.EventCals.Update(e);
            _context.SaveChanges();
            return true;
        }
        return false;
        
    }

}