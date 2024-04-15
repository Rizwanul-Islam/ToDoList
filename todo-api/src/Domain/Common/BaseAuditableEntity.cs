namespace ToDoService.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; } = default;

    public string? CreatedBy { get; set; }

    public DateTime? LastModified { get; set; } = default;
}
