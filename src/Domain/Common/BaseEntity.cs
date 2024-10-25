using Domain.Interfaces;

namespace Domain.Common;

public abstract class BaseEntity<TId>(TId id) : BaseAuditableEntity, IHasKey<TId>
{
    public TId Id { get; set; } = id;
}