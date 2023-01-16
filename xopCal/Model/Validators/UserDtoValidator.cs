using FluentValidation;
using xopCal.Entity;

namespace xopCal.Model.Validators;

public class UserDtoValidator : AbstractValidator<UserDtoIn>
{
    public UserDtoValidator(EventDbContext dbContext)
    {

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(6);

        RuleFor(x => x.ConfirmPassword)
            .Equal(e => e.Password);
        
        RuleFor(x => x.Email)
            .Custom((value, context) =>
                {
                    var emailDb = dbContext.Users.Any(u => u.Email == value);
                    if(emailDb)
                    {
                        context.AddFailure("Email","Email cannot be repeated");
                    }
                }
            );

        RuleFor(x => x.Color).Length(6);

        // "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"



    }
}