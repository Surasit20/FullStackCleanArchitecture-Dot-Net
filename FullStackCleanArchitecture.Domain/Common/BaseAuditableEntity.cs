namespace FullStackCleanArchitecture.Domain.Common;

public abstract class BaseAuditableEntity 
{
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public bool IsDelete { get; set; }
}
