﻿using System;
using System.Linq;
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

            var result = _clientRepository.Add(client);
            return result;
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

        private bool ClientValidator(Client client)
        {
            var clientValidator = new ClientValidator();
            client.ValidationResult = clientValidator.Validate(client);
            return client.ValidationResult.Errors.Any();
        }
    }
}