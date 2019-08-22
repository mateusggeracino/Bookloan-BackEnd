using System;
using System.Collections.Generic;
using System.Security.Claims;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces.Base;

namespace MGG.Bookloan.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetByKey(Guid key);
        Client GetBySocialNumber(string socialNumber);
        Client Login(Client client);
        IEnumerable<Claim> GetClaims(int clientId);
        void AddClaims(int clientId, Claim claims);
    }
}