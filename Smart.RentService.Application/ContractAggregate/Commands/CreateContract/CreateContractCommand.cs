using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;

namespace Smart.RentService.Application.ContractAggregate.Commands.CreateContract
{
    public class CreateContractCommand : IRequest<Result<ContractDto>>
    {
        public CreateContractCommand(Guid premiseCode, Guid equipmentCode, int equipmentCount)
        {
            PremiseCode = premiseCode;
            EquipmentCode = equipmentCode;
            EquipmentCount = equipmentCount;
        }

        public Guid PremiseCode { get; set; }
        public Guid EquipmentCode { get; set; }
        public int EquipmentCount { get; set; }
    }
}
