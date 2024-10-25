using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Reservations.Constans;
using Domain.Entities;

namespace Application.Features.Reservations.Rules;

public sealed class ReservationBusinessRules(IReservationRepository reservationRepository) : BaseBusinessRules
{
    public async Task<Reservation> EnsureReservationIdExists(Guid id, CancellationToken cancellationToken)
    {
        Reservation? reservation = await reservationRepository.GetAsync(
            predicate: p => p.Id == id,
            cancellationToken: cancellationToken);

        return reservation ?? throw new BusinessException(ReservationExceptionMessages.ReservationDoesNotExist);
    }

    public async Task EnsureNoDuplicateReservationForUpdate(Guid id, string customerEmail, DateTimeOffset reservationDate, Guid tableId, CancellationToken cancellationToken)
    {
        bool doesExists = await reservationRepository.AnyAsync(
            predicate: r => r.Id != id && r.CustomerEmail == customerEmail && r.ReservationDate == reservationDate && r.TableId == tableId,
            cancellationToken: cancellationToken);

        if (doesExists)
            throw new BusinessException(ReservationExceptionMessages.ReservationAlreadyExists);
    }

}
