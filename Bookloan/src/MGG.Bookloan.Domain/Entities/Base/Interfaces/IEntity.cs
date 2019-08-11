using System;

namespace MGG.Bookloan.Domain.Entities.Base.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        Guid UniqueKey { get; set; }
    }
}
