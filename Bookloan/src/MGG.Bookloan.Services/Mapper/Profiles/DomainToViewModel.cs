using AutoMapper;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Mapper.Profiles
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Client, ClientRequestViewModel>();
            CreateMap<Client, ClientResponseViewModel>();
            CreateMap<Book, BookResponseViewModel>();
            CreateMap<Book, BookRequestViewModel>();
            CreateMap<Loan, LoanRequestViewModel>();
            CreateMap<Loan, LoanRequestViewModel>();
        }
    }
}