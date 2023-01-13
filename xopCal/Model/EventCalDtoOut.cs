namespace xopCal.Model;

public class EventCalDtoOut
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartEvent { get; set; }

    public DateTime EndEvent { get; set; }
    
    public int OwnerId { get; set; }
    
    public string OwnerName { get; set; }
    
    public string OwnerEmail { get; set; }
    
}