using Application.Common.Exceptions;
using Application.Features.Reservations.Constans;
using Application.Features.Reservations.Rules;
using Application.IntegrationTests.Mocks.Repositories;

namespace Application.IntegrationTests.Features.Reservations.Rules;

public class ReservationBusinessRulesTests(ReservationMockRepository repository) : IClassFixture<ReservationMockRepository>
{
    private readonly ReservationBusinessRules businessRules = new(
        repository.MockRepository.Object);

    [Fact]
    public async Task EnsureReservationIdExists_Should_ThrowBusinessException_ReservationDoesNotExist()
    {
        var reservationId = Guid.NewGuid();

        var exception = await Assert.ThrowsAsync<BusinessException>(() => repository.BusinessRules.EnsureReservationIdExists(reservationId, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.ReservationDoesNotExist, exception.Message);
    }
}
