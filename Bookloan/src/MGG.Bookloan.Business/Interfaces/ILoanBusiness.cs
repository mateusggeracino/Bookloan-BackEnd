﻿using System.Collections.Generic;
using MGG.Bookloan.Domain.Entities;

namespace MGG.Bookloan.Business.Interfaces
{
    public interface ILoanBusiness
    {
        Loan Add(Loan loan);
        IEnumerable<Loan> GetBySocialNumber(string socialNumber);
    }
}