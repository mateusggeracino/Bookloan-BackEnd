using System;
using System.Collections.Generic;
using MGG.Bookloan.Domain.Entities;

namespace MGG.Bookloan.Business.Interfaces
{
    public interface IBookBusiness
    {
        Book Add(Book book);
        IEnumerable<Book> GetAll();
        Book GetByKey(Guid key);
        Book Update(Book bookEntity);
        bool Inactivate(Book book);
    }
}