using Context.Entities.Event;
using FluentValidation;

namespace ITentikaTest.EventProcessor.Services.Models;

public class EventValidator : AbstractValidator<Event>
{
    public EventValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Time).NotNull();
    }
}