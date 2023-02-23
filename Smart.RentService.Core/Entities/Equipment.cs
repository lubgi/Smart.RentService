using Smart.RentService.SharedKernel;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.Core.Entities;

public class Equipment : EntityBase
{
    public Guid Code { get; set; }
    public string Name { get; set; }
    public double Area { get; set; }

    public ICollection<Contract> Contracts { get; set; }
}