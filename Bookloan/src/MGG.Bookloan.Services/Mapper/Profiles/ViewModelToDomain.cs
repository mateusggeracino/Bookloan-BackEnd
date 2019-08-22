using System;
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
            CreateMap<BookRequestViewModel, Book>();
            CreateMap<BookResponseViewModel, Book>();
            CreateMap<LoginRequestViewModel, Client>();
            CreateMap<LoanRequestViewModel, Loan>()
                .ForMember(dest => dest.ClientKey, opt => opt.MapFrom(x => x.ClientKey))
                .ForMember(dest => dest.Days, opt => opt.MapFrom(x => x.Days));

            CreateMap<LoanResponseViewModel, Loan>();
        }
    }
}