using System.Text.Json.Serialization;

namespace xopCal.Entity;

public class EventCal
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartEvent { get; set; }

    public DateTime EndEvent { get; set; }

    public string Color { get; set; }
    public virtual User Owner { get; set; }
    public int OwnerId { get; set; }

    public List<User> Subscribers { get; set; }

}