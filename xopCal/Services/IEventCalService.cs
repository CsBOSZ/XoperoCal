using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;

public interface IEventCalService
{
    EventCal? GetEventCalById(int? id);
    List<EventCal> GetAllEventCalByUserId(int userId);
    List<EventCal> GetAllEventCalByName(string name);
    List<EventCal> GetAllEventCalByTime(TimeDto dto);
    bool PostEventCal(EventCalDto dto,int userId);
    bool PutEventCal(EventCalDto dto,int userId);
    bool DeleteEventCal(int id,int userId);
}