using Application.Common.Exceptions;
using Application.Features.Reservations.Commands.UpdateConfirmation;
using Application.Features.Reservations.Constans;
using Application.IntegrationTests.Mocks.Repositories;

namespace Application.IntegrationTests.Features.Reservations.Commands;

public class UpdateConfirmationTests(ReservationMockRepository repository) : IClassFixture<ReservationMockRepository>
{
    private readonly UpdateConfirmationCommandHandler _handler = new(
            repository.MockRepository.Object,
            repository.MockUnitOfWork.Object,
            repository.Mapper,
            repository.BusinessRules);

    [Fact]
    public async Task Should_Update_Confirmation()
    {
        Guid reservationId = Guid.Parse("8f0629bd-d88e-47b4-9860-0a0172dc71f5");
        var command = new UpdateConfirmationCommand(reservationId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Should_ThrowException_When_ReservationDoesNotExist()
    {
        var reservationId = Guid.NewGuid();
        var command = new UpdateConfirmationCommand(reservationId);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(ReservationExceptionMessages.ReservationDoesNotExist, exception.Message);
    }

}