﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using MGG.Bookloan.Domain.Entities;

namespace MGG.Bookloan.Business.Interfaces
{
    public interface IClientBusiness
    {
        Client Add(Client client);
        Client GetByKey(Guid key);
        Client Update(Client clientEntity);
        bool Inactivate(Client client);
        Client GetBySocialNumber(string socialNumber);
        Client Login(Client client);
        IEnumerable<Claim> GetClaims(int resultId);
    }
}