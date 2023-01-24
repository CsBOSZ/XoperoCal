using xopCal.Entity;
using xopCal.Model;

namespace xopCal.Services;

public interface IEventCalService
{
    public Task StartWatch();

    public Task StartWatch(EventCal ec);
    public bool PutSnooze(int id, int userId);
    EventCalDtoOut? GetEventCalById(int? id);
    int GetStatus(int id,int userId);
    List<EventCalDtoOut> GetAllEventCalByUserId(int userId);
    List<EventCalDtoOut> GetAllEventCalByName(string name);
    List<EventCalDtoOut> GetAllEventCalByTime(TimeDto dto);
    List<EventCalDtoOut> GetAll();
    bool PostEventCal(EventCalDtoIn dtoIn,int userId);
    bool PutEventCal(EventCalDtoIn dtoIn,int userId);
    bool DeleteEventCal(int id,int userId);
    bool Subscribe(int id, int userId);
    public bool UnSubscribe(int id, int userId);
}