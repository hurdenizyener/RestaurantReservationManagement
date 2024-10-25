using Application.Common.Exceptions;
using Application.Features.Reservations.Commands.Update;
using Application.Features.Reservations.Constans;
using Application.Features.Tables.Service;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Moq;

namespace Application.IntegrationTests.Features.Reservations.Commands;


public class UpdateReservationTests : IClassFixture<ReservationMockRepository>
{
    private readonly UpdateReservationCommandHandler _handler;
    private readonly ReservationMockRepository _repository;
    private readonly Mock<ITableService> _mockTableService;

    public UpdateReservationTests(ReservationMockRepository repository)
    {
        _repository = repository;
        _mockTableService = new Mock<ITableService>();
        _handler = new UpdateReservationCommandHandler(
            _repository.MockRepository.Object,
            _mockTableService.Object,
            _repository.MockUnitOfWork.Object,
            _repository.Mapper,
            _repository.BusinessRules);
    }

    [Fact]
    public async Task Should_Update_Reservation()
    {
        Guid reservationId = Guid.Parse("8f0629bd-d88e-47b4-9860-0a0172dc71f5");
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");

        var command = new UpdateReservationCommand(
            reservationId,
            tableId,
            "testName",
            "+01234567788",
            "tetss@test.com",
            "testSpecialRequest",
            2,
            DateTimeOffset.Now.AddDays(1));

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        _repository.MockRepository.Verify(r => r.Update(It.IsAny<Reservation>()), Times.Once);
        _repository.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task Should_ThrowException_When_ReservationDoesNotExist()
    {
        Guid reservationId = Guid.NewGuid();
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");

        var command = new UpdateReservationCommand(
           reservationId,
           tableId,
           "testName",
           "+01234567788",
           "tetss@test.com",
           "testSpecialRequest",
           2,
           DateTimeOffset.Now.AddDays(1));

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.ReservationDoesNotExist, exception.Message);
    }

    [Fact]
    public async Task Should_ThrowException_When_TableDoesNotExist()
    {
        Guid reservationId = Guid.Parse("8f0629bd-d88e-47b4-9860-0a0172dc71f5");
        Guid tableId = Guid.NewGuid();

        var command = new UpdateReservationCommand(
            reservationId,
            tableId,
            "testName",
            "+01234567788",
            "tetss@test.com",
            "testSpecialRequest",
            2,
            DateTimeOffset.Now.AddDays(1));


        _mockTableService
            .Setup(s => s.EnsureTableIdExists(tableId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.TableDoesNotExist, exception.Message);
    }
}
