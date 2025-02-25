namespace JEPCO.Domain.Common;

public interface IDeletableEntity
{
    bool IsDeleted { get; set; }
}
