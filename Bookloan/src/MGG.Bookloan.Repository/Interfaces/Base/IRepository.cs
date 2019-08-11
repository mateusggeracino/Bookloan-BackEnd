using System.Collections.Generic;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Repository.Interfaces.Base
{
    public interface IRepository<T> where T : Entity
    {
        T Add(T obj);
        T Update(T obj);
        bool Remove(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}