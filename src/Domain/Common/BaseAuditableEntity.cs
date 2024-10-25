namespace Domain.Common;

public abstract class BaseAuditableEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }

}
