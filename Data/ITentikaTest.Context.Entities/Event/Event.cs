﻿namespace Context.Entities.Event;

public class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public EventTypeEnum Type { get; set; }
    private DateTime Time { get; set; }
}