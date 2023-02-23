using System.Runtime.InteropServices.JavaScript;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Smart.RentService.Application.ContractAggregate.Commands.CreateContract;
using Smart.RentService.Application.ContractAggregate.Queries.GetContracts;
using QueryContractDTOs = Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;
using CommandContractDTOs = Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;
using Ardalis.Result.AspNetCore;
using Smart.RentService.WebAPI.Responses;

namespace Smart.RentService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ContractsController : ControllerBase
    {
        private readonly ISender _mediatr;

        public ContractsController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QueryContractDTOs.ContractDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<QueryContractDTOs.ContractDto>>> GetContracts(CancellationToken cancellationToken)
        {
            return Ok(await _mediatr.Send(new GetContractsQuery(), cancellationToken));
        }
        
        [HttpPost]
        [TranslateResultToActionResult]
        [ProducesResponseType(typeof(CommandContractDTOs.ContractDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<CommandContractDTOs.ContractDto>> CreateContract(CreateContractCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediatr.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return result.ToActionResult(this);
            }

            var errorMessage = result.Errors.FirstOrDefault();

            if (result.Status is ResultStatus.NotFound)
            {
                return NotFound(new NotFoundResponse(errorMessage));
            }

            return UnprocessableEntity(new UnprocessableEntityResponse(errorMessage));
        }
        
    }
}
