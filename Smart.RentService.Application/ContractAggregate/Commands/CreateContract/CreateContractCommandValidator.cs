using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Smart.RentService.Application.ContractAggregate.Commands.CreateContract
{
    public class CreateContractCommandValidator : AbstractValidator<CreateContractCommand>
    {
        public CreateContractCommandValidator()
        {
            RuleFor(c => c.EquipmentCode).NotEmpty();

            RuleFor(c => c.PremiseCode).NotEmpty();

            RuleFor(c => c.EquipmentCount).GreaterThan(0);
        }
    }
}
