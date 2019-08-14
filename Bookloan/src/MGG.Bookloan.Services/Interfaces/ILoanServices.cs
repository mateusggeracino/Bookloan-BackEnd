﻿using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Interfaces
{
    public interface ILoanServices
    {
        LoanResponseViewModel Add(LoanRequestViewModel loan);
        object GetBySocialNumber(string socialNumber);
    }
}