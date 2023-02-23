using Smart.RentService.SharedKernel;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.Core.Entities;

public class Contract : AuditableEntityBase, IAggregateRoot
{
    public int PremiseId { get; set; }
    public Premise Premise { get; set; }
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } 
    public int EquipmentCount { get; set; }
}