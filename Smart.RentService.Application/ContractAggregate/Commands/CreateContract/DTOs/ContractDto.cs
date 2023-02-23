using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;
public record ContractDto(PremiseDto PremiseDto, EquipmentDto EquipmentDto, int EquipmentCount)
{
    public static implicit operator ContractDto(Contract contract) => new(contract.Premise, contract.Equipment, contract.EquipmentCount);
}

