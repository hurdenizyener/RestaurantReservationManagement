using Application.Common.Exceptions;
using Application.Features.Reservations.Constans;
using Application.Features.Reservations.Queries.GetById;
using Application.IntegrationTests.Mocks.Repositories;
using static Application.Features.Reservations.Queries.GetById.GetReservationByIdQuery;

namespace Application.IntegrationTests.Features.Reservations.Queries;

public class GetReservationByIdTests(ReservationMockRepository repository) : IClassFixture<ReservationMockRepository>
{
    private readonly GetReservationByIdQueryHandler _handler = new(
            repository.MockRepository.Object,
            repository.Mapper);


    [Fact]
    public async Task Should_Return_Table_By_Id()
    {
        Guid reservationId = Guid.Parse("8f0629bd-d88e-47b4-9860-0a0172dc71f5");
        var query = new GetReservationByIdQuery(reservationId);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(query.Id, result.Id);

    }

    [Fact]
    public async Task Should_ThrowNotFoundException_When_ReservationDoesNotExist()
    {
        var reservationId = Guid.NewGuid();

        var query = new GetReservationByIdQuery(reservationId);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.ReservationDoesNotExist, exception.Message);
    }
}