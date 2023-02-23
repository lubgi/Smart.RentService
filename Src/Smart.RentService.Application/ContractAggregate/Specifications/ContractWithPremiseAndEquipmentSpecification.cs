using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Specifications
{
    public class ContractWithPremiseAndEquipmentSpecification : Specification<Contract>
    {
        public ContractWithPremiseAndEquipmentSpecification()
        {
            Query.Include(c => c.Premise);
            Query.Include(c => c.Equipment);
        }
    }
}
