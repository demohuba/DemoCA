namespace JEPCO.Domain.Common;

/// <summary>
/// <see cref="IDeletableEntity"/>
/// <see cref="IAuditableEntity"/>
/// </summary>
public class DeletableBaseEntity : BaseEntity, IDeletableEntity
{
    public bool IsDeleted { get; set; } = false;
}
/// <summary>
/// <see cref="IAuditableEntity"/>
/// </summary>
public class BaseEntity : IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
