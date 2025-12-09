using AspLessons.Abstractions;
using AspLessons.Contracts;
using FluentValidation;
namespace AspLessons.Helpers

{
    public class UserValidation : AbstractValidator<CreateUserRequest>
    {
        public UserValidation(IPhoneValidator phoneValidator)
        {

            RuleFor(user => user.Name)
                .NotEmpty( )
                .Matches("^[a-zа-яA-ZА-я0-9_]{2,}$")
                .WithMessage("Incorrect name");
            RuleFor(user => user.PhoneNumber).MustAsync(async (value, ctx) =>
                (await phoneValidator.ValidatePhone(value)).Valid)
                .WithMessage("Incorrect phone");
            RuleFor(user => user.Password)
                .NotEmpty( )
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must be between 8 and 20 characters, at least one digit, special symbol, and upper case letter.");
        }
    }
}
