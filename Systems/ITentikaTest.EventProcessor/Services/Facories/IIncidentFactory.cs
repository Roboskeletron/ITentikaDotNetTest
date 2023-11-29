using Context.Entities.Event;
using Context.Entities.Incident;

namespace ITentikaTest.EventProcessor.Services.Facories;

public interface IIncidentFactory
{
    event EventHandler<IncidentEventArgs> IncidentBuildCompleted; 
    void PushEvent(Event processingEvent);
}

public class IncidentEventArgs : EventArgs
{
    public IncidentEventArgs(Incident incident)
    {
        Incident = incident;
    }

    public Incident Incident { get; private set; }
}