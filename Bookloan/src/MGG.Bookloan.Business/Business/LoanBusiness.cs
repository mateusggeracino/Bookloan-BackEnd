using System.Collections.Generic;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces;

namespace MGG.Bookloan.Business.Business
{
    public class LoanBusiness : ILoanBusiness
    {
        private readonly ILoanRepository _loanRepository;

        public LoanBusiness(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public Loan Add(Loan loan)
        {
            return _loanRepository.Add(loan);
        }

        public IEnumerable<Loan> GetBySocialNumber(string socialNumber)
        {
            return _loanRepository.GetBySocialNumber(socialNumber);
        }
    }
}