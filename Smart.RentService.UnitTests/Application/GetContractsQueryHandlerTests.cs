using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Smart.RentService.Application.ContractAggregate.Queries.GetContracts;
using Smart.RentService.Application.ContractAggregate.Queries.GetContracts.DTOs;
using Smart.RentService.Application.ContractAggregate.Specifications;
using Smart.RentService.Core.Entities;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.UnitTests.Application
{
    public class GetContractsQueryHandlerTests
    {
        private readonly Mock<IReadRepository<Contract>> _contractRepositoryMock;
        private readonly GetContractsQueryHandler _handler;
        private readonly GetContractsQuery _query;

        public GetContractsQueryHandlerTests()
        {
            _contractRepositoryMock = new ();
            _handler = new(_contractRepositoryMock.Object);
            _query = new();
        }

        [Fact]
        public async Task Handle_ReturnsListOfContracts_WhenContractsExist()
        {
            // Arrange
            var contracts = new List<Contract>
            {
                new() { Premise = new(), Equipment = new() },
                new() { Premise = new(), Equipment = new() }
            };

            var expectedContracts = contracts.Select(c => new ContractDto(c.Premise, c.Equipment, c.EquipmentCount)).ToList().AsReadOnly();

            _contractRepositoryMock.Setup(r => r.ListAsync(It.IsAny<ContractWithPremiseAndEquipmentSpecification>(), default))
                .ReturnsAsync(contracts);

            // Act
            var result = await _handler.Handle(_query, default);

            // Assert
            result.Should().BeEquivalentTo(expectedContracts);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoContractsExist()
        {
            // Arrange
            var contracts = new List<Contract>();

            _contractRepositoryMock.Setup(r => r.ListAsync(It.IsAny<ContractWithPremiseAndEquipmentSpecification>(), default))
                .ReturnsAsync(contracts);

            var expectedContracts = new List<ContractDto>().AsReadOnly();

            // Act
            var result = await _handler.Handle(_query, default);

            // Assert
            result.Should().BeEquivalentTo(expectedContracts);
        }
    }

}
