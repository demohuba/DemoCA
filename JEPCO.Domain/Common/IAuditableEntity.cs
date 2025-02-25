namespace JEPCO.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    DateTime LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}
