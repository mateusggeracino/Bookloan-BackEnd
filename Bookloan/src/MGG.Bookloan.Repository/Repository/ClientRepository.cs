using System;
using Dapper;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository.Base;
using Microsoft.Extensions.Configuration;

namespace MGG.Bookloan.Repository.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(IConfiguration config) : base(config)
        {
        }
         
        public Client GetByKey(Guid key)
        {
            var query = "SELECT * FROM Client WHERE UniqueKey = @key";

            return Conn.QueryFirst<Client>(query, new { key });
        }

        public Client GetBySocialNumber(string socialNumber)
        {
            var query = "SELECT * FROM Client WHERE SocialNumber = @socialNumber";

            return Conn.QueryFirst<Client>(query, new { socialNumber });
        }

        public Client Login(Client client)
        {
            var query = "SELECT * FROM Client WHERE SocialNumber = @socialNumber AND  Password = @password";

            return Conn.QueryFirst<Client>(query,
                new {@socialNumber = client.SocialNumber, @password = client.Password});
        }
    }
}