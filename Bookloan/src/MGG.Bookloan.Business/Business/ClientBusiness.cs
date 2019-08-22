using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using MGG.Bookloan.Business.Interfaces;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Domain.Validations;
using MGG.Bookloan.Repository.Interfaces;

namespace MGG.Bookloan.Business.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;

        public ClientBusiness(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client Add(Client client)
        {
            if (ClientValidator(client)) return client;

            client.Active = true;
            var result = _clientRepository.Add(client);

            AddClaims(result);

            return result;
        }

        private void AddClaims(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim("ClientKey",client.UniqueKey.ToString()),
                new Claim("Client", "Update"),
                new Claim("Client", "Delete"),
                new Claim("Client", "Get"),
            };

            foreach(var claim in claims)
                _clientRepository.AddClaims(client.Id, claim);
        }

        public Client GetByKey(Guid key)
        {
            return _clientRepository.GetByKey(key);
        }

        public Client Update(Client client)
        {
            if (ClientValidator(client)) return client;

            return _clientRepository.Update(client);
        }

        public bool Inactivate(Client client)
        {
            client.Active = false;
            _clientRepository.Update(client);
            return true;
        }

        public Client GetBySocialNumber(string socialNumber)
        {
            return _clientRepository.GetBySocialNumber(socialNumber);
        }

        public Client Login(Client client)
        {
            return _clientRepository.Login(client);
        }

        public IEnumerable<Claim> GetClaims(int clientId)
        {
            return _clientRepository.GetClaims(clientId);
        }

        private bool ClientValidator(Client client)
        {
            var clientValidator = new ClientValidator();
            client.ValidationResult = clientValidator.Validate(client);
            return client.ValidationResult.Errors.Any();
        }
    }
}