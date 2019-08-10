using System;
using MGG.Bookloan.Domain.Entities.Base.Interfaces;

namespace MGG.Bookloan.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
        public Guid Key { get; set; }
    }
}
