using System.ComponentModel.DataAnnotations.Schema;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    [Table("Client")]
    public class Client : Entity
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
