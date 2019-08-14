using AutoMapper;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Mapper.Profiles
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            CreateMap<ClientRequestViewModel, Client>();
            CreateMap<ClientResponseViewModel, Client>();
            CreateMap<BookResponseViewModel, Book>();
            CreateMap<BookResponseViewModel, Book>();
            CreateMap<LoanResponseViewModel, Loan>();
            CreateMap<LoanResponseViewModel, Loan>();
        }
    }
}