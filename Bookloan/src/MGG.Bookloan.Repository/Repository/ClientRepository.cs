using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Context;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository.Base;

namespace MGG.Bookloan.Repository.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ConnectionFactory factory) : base(factory)
        {
        }
    }
}