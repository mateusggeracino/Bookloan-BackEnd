using System;
using System.Collections.Generic;

namespace MGG.Bookloan.Services.ViewModels.Request
{
    public class LoanRequestViewModel
    {
        public int Days { get; set; }
        public Guid ClientKey { get; set; }
        public List<Guid> BookKey { get; set; }
    }
}