using Context.Entities.Event;
using Context.Entities.Incident;

namespace ITentikaTest.EventProcessor.Services.Facories;

public class IncidentFactory : IIncidentFactory
{
    private Incident? incident = null;

    public event EventHandler<IncidentEventArgs> IncidentBuildCompleted;

    public void PushEvent(Event processingEvent)
    {
        if (processingEvent.Type is EventTypeEnum.Type3 or EventTypeEnum.Type4)
        {
            return;
        }

        switch (processingEvent.Type)
        {
            case EventTypeEnum.Type1:
                ProcessType1(processingEvent);
                break;
            case EventTypeEnum.Type2:
                ProcessType2(processingEvent);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void ProcessType1(Event processingEvent)
    {
        incident ??= new Incident()
        {
            Type = IncidentTypeEnum.Type1,
            Events = new List<Event>()
        };

        if (incident.Type == IncidentTypeEnum.Type2)
        {
            var deltaTime = processingEvent.Time - incident.Time;

            if (deltaTime.Seconds > 20)
            {
                incident = null;
                ProcessType1(processingEvent);
                return;
            }
        }
        
        incident.Events.Add(processingEvent);
        IncidentBuildCompleted.Invoke(this, new IncidentEventArgs(incident));
        incident = null;
    }

    private void ProcessType2(Event processingEvent)
    {
        incident = new Incident()
        {
            Type = IncidentTypeEnum.Type2,
            Events = new List<Event>()
            {
                processingEvent
            }
        };
    }
}