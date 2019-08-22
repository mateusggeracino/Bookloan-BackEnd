using MGG.Bookloan.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MGG.Bookloan.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Loan Add(Loan loan, IEnumerable<Guid> books);
        IEnumerable<Loan> GetBySocialNumber(string socialNumber);
        IEnumerable<Loan> GetByClientKey(Guid clientKey);
    }
}