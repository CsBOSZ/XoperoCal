using FluentValidation;

namespace xopCal.Model.Validators;

public class EventCalDtoValidator : AbstractValidator<EventCalDtoIn>
{
    
    public EventCalDtoValidator()
    {
        
        RuleFor(x =>new{ x.StartEvent , x.EndEvent})
            .Custom((value, context) =>
                {
                    if(value.EndEvent is not null && value.StartEvent > value.EndEvent)
                    {
                        context.AddFailure("EndEvent","EndEvent < StartEvent");
                    }
                }
            );
        
    }
    
    
}