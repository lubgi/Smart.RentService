using Ardalis.Result;
using MediatR;
using Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;
using Smart.RentService.Application.ContractAggregate.Specifications;
using Smart.RentService.Core.Entities;
using Smart.RentService.SharedKernel.Interfaces;


namespace Smart.RentService.Application.ContractAggregate.Commands.CreateContract
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContractCommand, Result<ContractDto>>
    {
        private readonly IRepository<Contract> _contractRepository;
        private readonly IReadRepository<Premise> _premiseRepository;
        private readonly IReadRepository<Equipment> _equipmentRepository;

        public CreateContractCommandHandler(IRepository<Contract> contractRepository, IReadRepository<Premise> premiseRepository, IReadRepository<Equipment> equipmentRepository)
        {
            _contractRepository = contractRepository;
            _premiseRepository = premiseRepository;
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Result<ContractDto>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
        {
            var equipment =
                await _equipmentRepository.FirstOrDefaultAsync(new EquipmentByCodeSpecification(request.EquipmentCode), cancellationToken);

            if (equipment is null)
            {
                return Result<ContractDto>.NotFound("Equipment not found");
            }

            var premise =
                await _premiseRepository.FirstOrDefaultAsync(new PremiseByCodeSpecification(request.PremiseCode), cancellationToken);

            if (premise is null)
            {
                return Result<ContractDto>.NotFound("Premise not found");
            }

            var areaSpecification = new AreaIsSufficientForEquipmentSpecification(equipment, request.EquipmentCount);
            var isAreSufficient = areaSpecification.IsSatisfiedBy(premise);

            if (!isAreSufficient)
            {
                return Result.Error("Not enough area to place equipment");
            }

            var contract = new Contract()
            {
                Premise = premise,
                Equipment = equipment,
                EquipmentCount = request.EquipmentCount
            };

            await _contractRepository.AddAsync(contract, cancellationToken);

            return Result.Success<ContractDto>(contract);
        }
    }
}
