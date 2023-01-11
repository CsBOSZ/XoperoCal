using FluentValidation;
using xopCal.Entity;

namespace xopCal.Model.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator(EventDbContext dbContext)
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(6);
        
    }
    
    
}