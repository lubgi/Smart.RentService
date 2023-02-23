
using System.Text.Json.Serialization;
using Smart.RentService.Core.Entities;

namespace Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs
{
    public record ContractDto(
        [property:JsonPropertyName("premise")]PremiseDto PremiseDto,
        [property: JsonPropertyName("equipment")] EquipmentDto EquipmentDto, 
        int EquipmentCount)
    {
        public static implicit operator ContractDto(Contract contract) => new(contract.Premise, contract.Equipment, contract.EquipmentCount);
    }
}
