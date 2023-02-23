using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Ardalis.Specification;
using FluentAssertions;
using Moq;
using Smart.RentService.Application.ContractAggregate.Commands.CreateContract;
using Smart.RentService.Application.ContractAggregate.Commands.CreateContract.DTOs;
using Smart.RentService.Application.ContractAggregate.Specifications;
using Smart.RentService.Core.Entities;
using Smart.RentService.SharedKernel.Interfaces;

namespace Smart.RentService.UnitTests.Application
{
    public class CreateContractCommandHandlerTests
    {
        private readonly Mock<IRepository<Contract>> _contractRepositoryMock;
        private readonly Mock<IReadRepository<Premise>> _premiseRepositoryMock;
        private readonly Mock<IReadRepository<Equipment>> _equipmentRepositoryMock;
        private readonly CreateContractCommandHandler _handler;
        private readonly CreateContractCommand _validCommand;

        public CreateContractCommandHandlerTests()
        {
            _contractRepositoryMock = new();
            _premiseRepositoryMock = new();
            _equipmentRepositoryMock = new();

            _handler = new CreateContractCommandHandler(_contractRepositoryMock.Object, _premiseRepositoryMock.Object, _equipmentRepositoryMock.Object);
            _validCommand = new CreateContractCommand(Guid.Empty, Guid.Empty, 2);
        }

        [Fact]
        public async Task Handle_WithInsufficientArea_ReturnsError()
        {
            // Arrange
            var equipment = new Equipment { Area = 10 };
            var premise = new Premise { Area = 10, Contracts = new List<Contract> { new Contract { Equipment = equipment, EquipmentCount = 2 } } };
            _equipmentRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<EquipmentByCodeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(equipment);
            _premiseRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<PremiseByCodeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(premise);

            // Act
            var result = await _handler.Handle(_validCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Errors.First().Should().BeEquivalentTo("Not enough area to place equipment");
        }

        [Fact]
        public async Task Handle_WithNonExistentEquipment_ReturnsNotFound()
        {
            // Arrange
            _equipmentRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<EquipmentByCodeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(Equipment));

            // Act
            var result = await _handler.Handle(_validCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.First().Should().BeEquivalentTo("Equipment not found");
        }

        [Fact]
        public async Task Handle_WithNonExistentPremise_ReturnsNotFound()
        {
            // Arrange
            _equipmentRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<EquipmentByCodeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Equipment());
            _premiseRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<PremiseByCodeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(Premise));

            // Act
            var result = await _handler.Handle(_validCommand, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Status.Should().Be(ResultStatus.NotFound);
            result.Errors.First().Should().BeEquivalentTo("Premise not found");
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccessResult()
        {
            // Arrange
            var equipment = new Equipment { Code = Guid.Empty, Area = 10, Name = "Equipment1"};
            var premise = new Premise { Code = Guid.Empty, Area = 20, Name = "Premise1", Contracts = new List<Contract>()};
            var resultContract = new ContractDto(premise, equipment, _validCommand.EquipmentCount);

            _equipmentRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<EquipmentByCodeSpecification>(), default))
                .ReturnsAsync(equipment);

            _premiseRepositoryMock.Setup(r => r.FirstOrDefaultAsync(It.IsAny<PremiseByCodeSpecification>(), default))
                .ReturnsAsync(premise);

            var areaSpecification = new AreaIsSufficientForEquipmentSpecification(equipment, _validCommand.EquipmentCount);

            _premiseRepositoryMock.Setup(r => r.FirstOrDefaultAsync(areaSpecification, default))
                .ReturnsAsync(premise);

            // Act
            var result = await _handler.Handle(_validCommand, default);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Status.Should().Be(ResultStatus.Ok);
            result.Value.Should().BeEquivalentTo(resultContract);

            _contractRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Contract>(), default), Times.Once);
        }

    }
}
