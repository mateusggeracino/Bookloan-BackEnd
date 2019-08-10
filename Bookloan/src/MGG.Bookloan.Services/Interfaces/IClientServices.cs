using MGG.Bookloan.Services.ViewModels.Request;

namespace MGG.Bookloan.Services.Interfaces
{
    public interface IClientServices
    {
        ClientRequestViewModel Add(ClientRequestViewModel client);
    }
}