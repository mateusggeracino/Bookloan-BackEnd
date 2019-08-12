using MGG.Bookloan.Domain.Entities.Base;

namespace MGG.Bookloan.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public bool Active { get; set; }
    }
}
