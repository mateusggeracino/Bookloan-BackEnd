using System;
using MGG.Bookloan.Domain.Entities;

namespace MGG.Bookloan.Business.Interfaces
{
    public interface IClientBusiness
    {
        Client Add(Client client);
        Client GetByKey(Guid key);
        Client Update(Client clientEntity);
        bool Inactivate(Client client);
    }
}