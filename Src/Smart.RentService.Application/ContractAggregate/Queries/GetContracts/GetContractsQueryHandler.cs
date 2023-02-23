using MediatR;
using Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;
using Smart.RentService.Application.ContractAggregate.Specifications;
using Smart.RentService.Core.Entities;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.Application.ContractAggregate.Queries.GetContracts
{
    public class GetContractsQueryHandler : IRequestHandler<GetContractsQuery, IReadOnlyList<ContractDto>>
    {
        private readonly IReadRepository<Contract> _contractRepository;

        public GetContractsQueryHandler(IReadRepository<Contract> contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<IReadOnlyList<ContractDto>> Handle(GetContractsQuery request, CancellationToken cancellationToken)
        {
            var specification = new ContractWithPremiseAndEquipmentSpecification();
            var contracts = await _contractRepository.ListAsync(specification,cancellationToken);

            return contracts.Select<Contract, ContractDto>(c => c).ToList().AsReadOnly();
        }
    }
}
