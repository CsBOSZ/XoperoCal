using System.Text.RegularExpressions;
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
        
        RuleFor(x => x.Color)  
            .Custom((value, context) =>
                {
                    if(value is not null && !Regex.IsMatch(value,"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"))
                    {
                        context.AddFailure("Color","Color is not a color");
                    }
                }
            );
        
    }
    
    
}