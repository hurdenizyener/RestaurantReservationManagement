using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Reservations.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reservations.Commands.UpdateConfirmation;

public sealed record UpdateConfirmationCommand(Guid Id) : IRequest<Result>;

public sealed class UpdateConfirmationCommandHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ReservationBusinessRules reservationBusinessRules) : IRequestHandler<UpdateConfirmationCommand, Result>
{
    public async Task<Result> Handle(UpdateConfirmationCommand request, CancellationToken cancellationToken)
    {

        Reservation reservation = await reservationBusinessRules.EnsureReservationIdExists(request.Id, cancellationToken);

        reservation = mapper.Map(request, reservation);
        reservation.IsConfirmed = !reservation.IsConfirmed;

        reservationRepository.Update(reservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
