using Dapper;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository.Base;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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

            return Conn.QueryFirstOrDefault<Client>(query, new { key });
        }

        public Client GetBySocialNumber(string socialNumber)
        {
            var query = "SELECT * FROM Client WHERE SocialNumber = @socialNumber";

            return Conn.QueryFirstOrDefault<Client>(query, new { socialNumber });
        }

        public Client Login(Client client)
        {
            var query = "SELECT * FROM Client WHERE SocialNumber = @socialNumber AND  Password = @password";

            return Conn.QueryFirstOrDefault<Client>(query,
                new { @socialNumber = client.SocialNumber, @password = client.Password });
        }

        public IEnumerable<Claim> GetClaims(int clientId)
        {
            var query = "SELECT ClaimType as type, ClaimValue as value FROM [dbo].[ClientClaims] WHERE ClientId = @clientId";

            return Conn.Query<Claim>(query, new { clientId });
        }
    }
}