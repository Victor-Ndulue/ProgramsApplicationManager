namespace Domain.Models.AuditBaseEntity;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
