using FluentValidation;
using MGG.Bookloan.Domain.Entities;

namespace MGG.Bookloan.Domain.Validations
{
    public class LoanValidator : AbstractValidator<Loan>
    {
        public LoanValidator()
        {
            
        }
    }
}