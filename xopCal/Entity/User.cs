namespace xopCal.Entity;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }

    public virtual List<EventCal> EventCals { get; set; } = new List<EventCal>();

}