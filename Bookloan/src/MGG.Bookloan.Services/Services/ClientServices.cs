using AutoMapper;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;

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

        public ClientRequestViewModel Add(ClientRequestViewModel client)
        {
            var clientEntity = _mapper.Map<Client>(client);
            var result = _clientBusiness.Add(clientEntity);

            return _mapper.Map<ClientRequestViewModel>(result);
        }
    }
}