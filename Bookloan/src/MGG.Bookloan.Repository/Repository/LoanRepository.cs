using System.Collections.Generic;
using Dapper;
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

        public IEnumerable<Loan> GetBySocialNumber(string socialNumber)
        {
            var query = 
                        "SELECT * FROM Loan lo " +
                        " INNER JOIN Client cl on cl.Id = lo.ClientId " +
                        " WHERE Client.SocialNumber = @socialNumber";

            return Conn.Query<Loan>(query, new {socialNumber});
        }
    }
}