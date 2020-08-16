using DevNots.Application.Contracts;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Username)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(32);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
