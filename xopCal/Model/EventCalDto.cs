namespace xopCal.Model;

public class EventCalDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartEvent { get; set; }

    public DateTime? EndEvent { get; set; }
}