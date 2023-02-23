using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;

public record PremiseDto(string Name)
{
    public static implicit operator PremiseDto(Premise premise) => new(premise.Name);
}