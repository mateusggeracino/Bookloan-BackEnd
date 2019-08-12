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
    }
}