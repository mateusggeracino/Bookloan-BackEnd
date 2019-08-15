using System;
using System.Collections.Generic;
using System.Linq;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Domain.Validations;
using MGG.Bookloan.Repository.Interfaces;

namespace MGG.Bookloan.Business.Business
{
    public class LoanBusiness : ILoanBusiness
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBookRepository _bookRepository;
        public LoanBusiness(ILoanRepository loanRepository, IClientRepository clientRepository, IBookRepository bookRepository)
        {
            _loanRepository = loanRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
        }

        public Loan Add(Loan loan, IEnumerable<Guid> books)
        {
            if (LoanValidator(loan)) return loan;

            var client = _clientRepository.GetByKey(loan.ClientKey);
            loan.ClientId = client.Id;
            foreach (var bookKey in books)
            {
                var book = _bookRepository.GetByKey(bookKey);
                loan.BookId = book.Id;
                _loanRepository.Add(loan);
            }
            
            return loan;
        }

        private bool LoanValidator(Loan loan)
        {
            var loanValidator = new LoanValidator();
            loan.ValidationResult = loanValidator.Validate(loan);

            return loan.ValidationResult.Errors.Any();
        }

        public IEnumerable<Loan> GetBySocialNumber(string socialNumber)
        {
            return _loanRepository.GetBySocialNumber(socialNumber);
        }
    }
}