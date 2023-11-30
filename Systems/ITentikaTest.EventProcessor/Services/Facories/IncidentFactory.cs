using Context.Entities.Event;
using Context.Entities.Incident;
using ITentikaTest.EventProcessor.settings;

namespace ITentikaTest.EventProcessor.Services.Facories;

public class IncidentFactory : IIncidentFactory
{
    private readonly ILogger<IncidentFactory> logger;
    private readonly IncidentFactrorySettings settings;
    private Incident? incident = null;

    public IncidentFactory(ILogger<IncidentFactory> logger, IncidentFactrorySettings settings)
    {
        this.logger = logger;
        this.settings = settings;
    }

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

            if (deltaTime.Seconds > Math.Max(1, settings.CreationTimeRange))
            {
                logger.LogTrace("Incident type 2 creation failed");
                
                incident.Type = IncidentTypeEnum.Type1;
                incident.Events.Clear();
            }
        }
        
        incident.Events.Add(processingEvent);
        
        logger.LogTrace("Building of {@incident} completed", incident);
        
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
        
        logger.LogTrace("Type 2 incident {@incident} created", incident);
    }
}