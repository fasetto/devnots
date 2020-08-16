using DevNots.Application.Contracts;
using FluentValidation;

namespace DevNots.Application.Validations
{
    public class UpdateNoteValidator: AbstractValidator<UpdateNoteRequest>
    {
        public UpdateNoteValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.Text)
                .NotEmpty();
        }
    }
}
