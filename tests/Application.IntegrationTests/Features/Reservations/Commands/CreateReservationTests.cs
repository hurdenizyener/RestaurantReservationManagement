using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Models.Emails;
using Application.Contracts.Infrastructure.Services;
using Application.Features.Reservations.Commands.Create;
using Application.Features.Reservations.Constans;
using Application.Features.Tables.Service;
using Application.IntegrationTests.Mocks.Data;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Moq;
using static Application.Features.Reservations.Commands.Create.CreateReservationCommand;

namespace Application.IntegrationTests.Features.Reservations.Commands;

public class CreateReservationTests : IClassFixture<ReservationMockRepository>
{
    private readonly CreateReservationCommandHandler _handler;
    private readonly ReservationMockRepository _repository;
    private readonly Mock<ITableService> _mockTableService;
    private readonly Mock<IEmailNotificationService> _mockEmailService;

    public CreateReservationTests(ReservationMockRepository repository)
    {
        _repository = repository;
        _mockTableService = new Mock<ITableService>();
        _mockEmailService = new Mock<IEmailNotificationService>();
        _handler = new CreateReservationCommandHandler(
            _repository.MockRepository.Object,
            _mockTableService.Object,
            _repository.MockUnitOfWork.Object,
            _repository.Mapper,
            _mockEmailService.Object);
    }

    [Fact]
    public async Task Should_Create_ReservationAndSendMail()
    {
        var reservationDate = DateTimeOffset.Now.AddHours(1);
        var guestCount = 4;
        var command = new CreateReservationCommand(
            "testName",
            "+01234567788",
            "tetss@test.com",
            "testSpecialRequest",
            guestCount,
            reservationDate);

        var availableTables = TableData.GetInitialData().Where(t => t.IsAvailable && t.Capacity >= guestCount).ToList();


        _mockTableService
            .Setup(s => s.FindAvailableTablesForReservation(reservationDate, guestCount, It.IsAny<CancellationToken>()))
            .ReturnsAsync(availableTables);

        var selectedTable = availableTables[0];

        var email = new ReservationEmail(command.CustomerEmail, command.CustomerName, selectedTable.Number, command.ReservationDate, command.GuestCount);

        _mockEmailService
           .Setup(s => s.SendReservationEmailAsync(It.IsAny<ReservationEmail>()))
           .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Rezervasyon Başarıyla Yapıldı.", result.Message);
        Assert.Equal(selectedTable.Number, availableTables[0].Number);


        _mockEmailService.Verify(e => e.SendReservationEmailAsync(It.IsAny<ReservationEmail>()), Times.Once);
        _repository.MockRepository.Verify(r => r.AddAsync(It.IsAny<Reservation>()), Times.Once);
        _repository.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }


    [Fact]
    public async Task Should_ThrowException_When_NoAvailableTables()
    {
        var reservationDate = DateTimeOffset.Now.AddHours(1);
        var guestCount = 8;
        var command = new CreateReservationCommand(
            "testName",
            "+01234567788",
            "tetss@test.com",
            "testSpecialRequest",
            guestCount,
            reservationDate);

        var availableTables = TableData.GetInitialData().Where(t => t.IsAvailable && t.Capacity >= guestCount).ToList();

        _mockTableService
            .Setup(s => s.FindAvailableTablesForReservation(reservationDate, guestCount, It.IsAny<CancellationToken>()))
            .ReturnsAsync(availableTables);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.NoAvailableTables, exception.Message);
    }

  
}