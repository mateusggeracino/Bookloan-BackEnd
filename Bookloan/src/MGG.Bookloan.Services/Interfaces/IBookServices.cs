using System;
using System.Collections.Generic;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;

namespace MGG.Bookloan.Services.Interfaces
{
    public interface IBookServices
    {
        BookResponseViewModel Add(BookRequestViewModel book);
        IEnumerable<BookResponseViewModel> GetAll();
        BookResponseViewModel Update(Guid key, BookRequestViewModel book);
        bool Inactivate(Guid key);
    }
}