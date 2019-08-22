using System;
using System.Linq;
using AutoMapper;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Services
{
    public class ClientServices : IClientServices
    { 
        private readonly IClientBusiness _clientBusiness;
        private readonly IMapper _mapper;

        public ClientServices(IClientBusiness clientBusiness, IMapper mapper)
        {
            _clientBusiness = clientBusiness;
            _mapper = mapper;
        }

        public ClientResponseViewModel Add(ClientRequestViewModel client)
        {
            var clientEntity = _mapper.Map<Client>(client);
            var result = _clientBusiness.Add(clientEntity);

            return _mapper.Map<ClientResponseViewModel>(result);
        }

        public ClientResponseViewModel GetByKey(Guid key)
        {
            var clientEntity = _clientBusiness.GetByKey(key);
            return _mapper.Map<ClientResponseViewModel>(clientEntity);
        }

        public ClientResponseViewModel Update(Guid key, ClientRequestViewModel client)
        {
            var clientConsult = _clientBusiness.GetByKey(key);

            var clientEntity = _mapper.Map<Client>(client);
            clientEntity.UniqueKey = key;
            clientEntity.Id = clientConsult.Id;

            var result = _clientBusiness.Update(clientEntity);
            return _mapper.Map<ClientResponseViewModel>(result);
        }

        public bool Inactivate(Guid key)
        {
            var client = _clientBusiness.GetByKey(key);
            return _clientBusiness.Inactivate(client);
        }

        public ClientResponseViewModel GetBySocialNumber(string socialNumber)
        {
            return _mapper.Map<ClientResponseViewModel>(_clientBusiness.GetBySocialNumber(socialNumber));
        }

        public LoginResponseViewModel Login(LoginRequestViewModel login)
        {
            var result = _clientBusiness.Login(_mapper.Map<Client>(login));
            var loginViewModel = _mapper.Map<LoginResponseViewModel>(result);

            if(result != null)
                loginViewModel.Claims = _clientBusiness.GetClaims(result.Id).ToList();

            return loginViewModel;
        }
    }
}