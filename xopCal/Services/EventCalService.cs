using Microsoft.EntityFrameworkCore;
using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;


public class EventCalService : IEventCalService
{
    private readonly EventDbContext _context;


    public EventCalService(EventDbContext context)
    {
        _context = context;
    }

    public EventCal? GetEventCalById(int? id)
    {
        if (id is null)
        {
            return null;
        }
        return _context.EventCals.FirstOrDefault(e => e.Id == id);
    }
    
    public List<EventCal> GetAllEventCalByUserId(int userId)
    {
        return _context.EventCals.Where(e => e.OwnerId == userId).ToList();
    }
    
    public List<EventCal> GetAllEventCalByTime(TimeDto dto)
    {
        if (dto.EndEvent is null)
        {
            return _context.EventCals.Where(e => e.StartEvent <= dto.StartEvent && e.EndEvent <= dto.StartEvent).ToList();
        }

        return _context.EventCals.Where(e => e.StartEvent >= dto.StartEvent && e.EndEvent >= dto.EndEvent).ToList();

    }
    
    public List<EventCal> GetAllEventCalByName(string name)
    {
        return _context.EventCals.Where(e => e.Name == name).ToList();
    }
    
    public bool PostEventCal(EventCalDto dto,int userId)
    {
        _context.EventCals.Add(new EventCal()
        {
            Name = dto.Name ?? "none",
            Description = dto.Description ?? "none",
            StartEvent = dto.StartEvent ?? DateTime.Now,
            EndEvent = dto.EndEvent ?? DateTime.Now.AddDays(1),
            OwnerId = userId,
        });
        _context.SaveChanges();
        return true;
    }
    
    public bool PutEventCal(EventCalDto dto,int userId)
    {
        EventCal? e = GetEventCalById(dto.Id);
        if (e is not null && e.OwnerId == userId)
        {
            e.Name = dto.Name ?? e.Name;
            e.Description = dto.Description ?? e.Description;
            e.StartEvent = dto.StartEvent ?? e.StartEvent;
            e.EndEvent = dto.EndEvent ?? e.EndEvent;
            _context.EventCals.Update(e);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool DeleteEventCal(int id,int userId)
    {
        var e = GetEventCalById(id);
        if (e is not null && e.OwnerId == userId)
        {
            _context.EventCals.Remove(e);
            _context.SaveChanges();
            return true;
        }
        return false;
    }


}