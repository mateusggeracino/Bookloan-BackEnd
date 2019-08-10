using FluentValidation;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Infra;

namespace MGG.Bookloan.Domain.Validations
{
    /// <summary>
    /// Classe responsável por validar as regras de persistencia de cliente
    /// </summary>
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage(Labels.ExceptionMinLength)
                .MaximumLength(60).WithMessage(Labels.ExceptionMaxLength)
                .NotEmpty().WithMessage(Labels.ExceptionNotEmpty);
        }
    }
}
