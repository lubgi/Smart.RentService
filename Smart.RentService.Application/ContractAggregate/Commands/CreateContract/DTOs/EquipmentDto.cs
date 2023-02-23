using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;

public record EquipmentDto(string Name)
{
    public static implicit operator EquipmentDto(Equipment equipment) => new(equipment.Name);
}