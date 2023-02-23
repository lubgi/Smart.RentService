namespace Smart.RentService.SharedKernel;

public abstract class AuditableEntityBase : EntityBase
{
    public DateTimeOffset Created { get; set; }

    public DateTimeOffset? LastModified { get; set; }
}