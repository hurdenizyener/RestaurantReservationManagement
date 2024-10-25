using Application.Common.Exceptions;
using Application.Features.Reservations.Constans;
using Application.Features.Reservations.Queries.GetAllList;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using static Application.Features.Reservations.Queries.GetAllList.GetAllReservationsQuery;

namespace Application.IntegrationTests.Features.Reservations.Queries;

public class GetAllReservationsTests(ReservationMockRepository repository) : IClassFixture<ReservationMockRepository>
{
    private readonly GetAllReservationsQueryHandler _handler = new(
            repository.MockRepository.Object,
            repository.Mapper);


    [Fact]
    public async Task Should_Rotate_All_Reservations()
    {
        var query = new GetAllReservationsQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, reservation => Assert.True(reservation.Id != Guid.Empty));
   
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_When_ReservationNotFound()
    {

        repository.MockRepository.Setup(repo =>
        repo.GetAllListAsync(
            It.IsAny<Expression<Func<Reservation, bool>>>(),
            It.IsAny<Func<IQueryable<Reservation>, IOrderedQueryable<Reservation>>>(),
            It.IsAny<Func<IQueryable<Reservation>, IIncludableQueryable<Reservation, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var query = new GetAllReservationsQuery();

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal(ReservationExceptionMessages.ReservationNotFound, exception.Message);
    }

}
