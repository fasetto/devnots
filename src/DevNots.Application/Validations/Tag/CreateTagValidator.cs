using DevNots.Application.Contracts;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class CreateTagValidator: AbstractValidator<CreateTagRequest>
    {
        public CreateTagValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Color)
                .NotEmpty()
                .MaximumLength(7);

            RuleFor(x => x.Name)
                .MaximumLength(32);
        }
    }
}
