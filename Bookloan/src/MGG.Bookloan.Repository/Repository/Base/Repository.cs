using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MGG.Bookloan.Domain.Entities.Base;
using MGG.Bookloan.Repository.Interfaces;

namespace MGG.Bookloan.Repository.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected IDbConnection Conn = new SqlConnection("");
        public T Add(T obj)
        {
            throw new System.NotImplementedException();
        }

        public T Update(T obj)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}