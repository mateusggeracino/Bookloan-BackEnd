using FluentValidation;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Infra;

namespace MGG.Bookloan.Domain.Validations
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .MinimumLength(2).WithMessage(Labels.ExceptionMinLength)
                .MaximumLength(90).WithMessage(Labels.ExceptionMaxLength)
                .NotEmpty().WithMessage(Labels.ExceptionNotEmpty);
        }
    }
}
