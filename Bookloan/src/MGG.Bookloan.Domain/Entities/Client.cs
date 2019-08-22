using System.ComponentModel.DataAnnotations.Schema;
using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    [Table("Client")]
    public class Client : Entity
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string SocialNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
