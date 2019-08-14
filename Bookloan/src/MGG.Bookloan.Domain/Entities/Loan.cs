using System.Collections.Generic;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    public class Loan : Entity
    {
        public int Days { get; set; }
        public Client Client { get; set; }
        public List<Book> Books { get; set; }
    }
}