using System;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces.Base;

namespace MGG.Bookloan.Repository.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetByKey(Guid key);
        Client GetBySocialNumber(string socialNumber);
        Client Login(Client client);
    }
}