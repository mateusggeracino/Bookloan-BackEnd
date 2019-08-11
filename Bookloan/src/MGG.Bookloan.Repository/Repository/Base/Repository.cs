using System.Collections.Generic;
using System.Data;
using Dapper.Contrib.Extensions;
using MGG.Bookloan.Domain.Entities.Base;
using MGG.Bookloan.Repository.Context;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Interfaces.Base;

namespace MGG.Bookloan.Repository.Repository.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected IDbConnection Conn;
        public Repository(ConnectionFactory factory)
        {
            Conn = factory.Conn;
        }

        public T Add(T obj)
        {
            Conn.Insert(obj);
            return obj;
        }

        public T Update(T obj)
        {
            Conn.Update(obj);
            return obj;
        }

        public bool Remove(int id)
        {
            Conn.Delete(GetById(id));
            return true;
        }

        public T GetById(int id)
        {
            return Conn.Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Conn.GetAll<T>();
        }
    }
}