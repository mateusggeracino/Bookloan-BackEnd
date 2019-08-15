using Dapper.Contrib.Extensions;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    [Table("Book")]
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
