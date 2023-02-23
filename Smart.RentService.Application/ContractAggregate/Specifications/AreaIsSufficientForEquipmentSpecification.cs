using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Specifications
{
    public class AreaIsSufficientForEquipmentSpecification : Specification<Premise>
    {
        public AreaIsSufficientForEquipmentSpecification(Equipment newEquipment, int newEquipmentCount)
        {
            Query.Include(p => p.Contracts);

            Query.Where(p => p.Area >= p.Contracts.Sum(c => c.EquipmentCount * c.Equipment.Area) + newEquipment.Area * newEquipmentCount);
        }
    }
}
