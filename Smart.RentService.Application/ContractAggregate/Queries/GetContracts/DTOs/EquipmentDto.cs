using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;

public record EquipmentDto(string Name)
{
    public static implicit operator EquipmentDto(Equipment equipment) => new(equipment.Name);
}