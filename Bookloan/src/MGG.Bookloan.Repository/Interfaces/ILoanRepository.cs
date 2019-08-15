using System.Collections.Generic;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces.Base;

namespace MGG.Bookloan.Repository.Interfaces
{
    public interface ILoanRepository : IRepository<Loan>
    {
        IEnumerable<Loan> GetBySocialNumber(string socialNumber);
    }
}