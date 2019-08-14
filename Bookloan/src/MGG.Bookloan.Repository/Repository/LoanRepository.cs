using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository.Base;
using Microsoft.Extensions.Configuration;

namespace MGG.Bookloan.Repository.Repository
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(IConfiguration config) : base(config)
        {
        }
    }
}