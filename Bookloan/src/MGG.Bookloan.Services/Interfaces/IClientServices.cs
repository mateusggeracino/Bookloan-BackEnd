using System;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Interfaces
{
    public interface IClientServices
    {
        ClientResponseViewModel Add(ClientRequestViewModel client);
        ClientResponseViewModel GetByKey(Guid key);
        ClientResponseViewModel Update(Guid key, ClientRequestViewModel client);
        bool Inactivate(Guid key);
        ClientResponseViewModel GetBySocialNumber(string socialNumber);
    }
}