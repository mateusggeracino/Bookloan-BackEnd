using System;
using Dapper;
using MGG.Bookloan.Domain.Entities;
using MGG.Bookloan.Repository.Interfaces;
using MGG.Bookloan.Repository.Repository.Base;
using Microsoft.Extensions.Configuration;

namespace MGG.Bookloan.Repository.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(IConfiguration config) : base(config)
        {
        }

        public Book GetByKey(Guid key)
        {
            var query = "SELECT * FROM Book WHERE UniqueKey = @key";
            return Conn.QueryFirst<Book>(query, new {key});
        }
    }
}