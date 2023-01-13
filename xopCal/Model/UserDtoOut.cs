using xopCal.Entity;

namespace xopCal.Model;

public class UserDtoOut
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public virtual List<EventCalDtoOut2> EventCals { get; set; }
}