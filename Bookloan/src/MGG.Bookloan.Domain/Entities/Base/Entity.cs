using System;
using Dapper.Contrib.Extensions;
using FluentValidation.Results;
using MGG.Bookloan.Domain.Entities.Base.Interfaces;

namespace MGG.Bookloan.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            UniqueKey = Guid.NewGuid();
        }

        public int Id { get; set; }
        public Guid UniqueKey { get; set; }

        [Write(false)]
        public ValidationResult ValidationResult { get; set; }
    }
}
