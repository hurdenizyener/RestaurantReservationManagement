using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Reservations.Rules;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reservations.Commands.Delete;

public sealed record DeleteReservationCommand(Guid Id) : IRequest<Result>
{
    public sealed class DeleteReservationCommandHandler(
        IReservationRepository reservationRepository,
        IUnitOfWork unitOfWork,
        ReservationBusinessRules reservationBusinessRules) : IRequestHandler<DeleteReservationCommand, Result>
    {
        public async Task<Result> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = await reservationBusinessRules.EnsureReservationIdExists(request.Id, cancellationToken);

            reservationRepository.Delete(reservation!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}