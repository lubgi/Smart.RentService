using MediatR;
using Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;

namespace Smart.RentService.Application.ContractAggregate.Queries.GetContracts;
public class GetContractsQuery : IRequest<IReadOnlyList<ContractDto>>
{
}

