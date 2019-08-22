using System;
using Dapper.Contrib.Extensions;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    [Table("Loan")]
    public sealed class Loan : Entity
    {
        public int Days { get; set; }
        public int ClientId { get; set; }
        public int BookId { get; set; }

        [Write(false)]
        public Guid ClientKey{ get; set; }
        [Write(false)]
        public Client Client { get; set; }
        [Write(false)]
        public Book Book { get; set; }
    }
}