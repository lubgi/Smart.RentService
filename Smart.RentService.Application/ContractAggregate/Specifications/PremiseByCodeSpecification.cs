using Ardalis.Specification;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Specifications;

public class PremiseByCodeSpecification : Specification<Premise>, ISingleResultSpecification<Premise>
{
    public PremiseByCodeSpecification(Guid premiseCode)
    {
        Query.Where(p => p.Code == premiseCode);
    }
}