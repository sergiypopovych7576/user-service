using System;

namespace Services.Shared.Domain.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }
    }
}
