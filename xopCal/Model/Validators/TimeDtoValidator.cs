using FluentValidation;
using xopCal.Entity;

namespace xopCal.Model.Validators;

public class TimeDtoValidator : AbstractValidator<TimeDto>
{
    public TimeDtoValidator()
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