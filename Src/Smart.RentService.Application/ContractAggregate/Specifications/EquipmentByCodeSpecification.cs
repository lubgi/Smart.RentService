using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Specifications
{
    public class EquipmentByCodeSpecification : Specification<Equipment>, ISingleResultSpecification<Equipment>
    {
        public EquipmentByCodeSpecification(Guid equipmentCode)
        {
            Query.Where(e => e.Code == equipmentCode);
        }
    }
}
