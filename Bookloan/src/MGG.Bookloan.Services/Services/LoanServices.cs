using System.Collections.Generic;
using AutoMapper;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Services
{
    public class LoanServices : ILoanServices
    {
        private readonly ILoanBusiness _loanBusiness;
        private readonly IMapper _mapper;
        public LoanServices(ILoanBusiness loanBusiness, IMapper mapper)
        {
            _loanBusiness = loanBusiness;
            _mapper = mapper;
        }

        public LoanResponseViewModel Add(LoanRequestViewModel loan)
        {
            var loanEntity = _mapper.Map<Loan>(loan);
            var result = _loanBusiness.Add(loanEntity);
            return _mapper.Map<LoanResponseViewModel>(result);
        }

        public IEnumerable<LoanResponseViewModel> GetBySocialNumber(string socialNumber)
        {
            var result = _loanBusiness.GetBySocialNumber(socialNumber);
            return _mapper.Map<IEnumerable<LoanResponseViewModel>>(result);
        }
    }
}