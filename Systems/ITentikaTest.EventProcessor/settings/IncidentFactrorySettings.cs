namespace ITentikaTest.EventProcessor.settings;

public class IncidentFactrorySettings
{
    /// <summary>
    /// Creation time range for the incident of 2 type in seconds
    /// </summary>
    public int CreationTimeRange { get; private set; } = 20;
}