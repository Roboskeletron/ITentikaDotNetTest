namespace Context.Entities.Incident;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public IncidentTypeEnum Type { get; set; }
    public DateTime Time { get; set; }
    public virtual ICollection<Event.Event> Events { get; set; }
}