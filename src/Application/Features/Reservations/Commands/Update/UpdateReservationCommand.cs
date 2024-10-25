using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Reservations.Constans;
using Application.Features.Reservations.Rules;
using Application.Features.Tables.Service;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reservations.Commands.Update;

public sealed record UpdateReservationCommand(
   Guid Id,
   Guid TableId,
   string CustomerName,
   string CustomerPhone,
   string CustomerEmail,
   string SpecialRequest,
   int GuestCount,
   DateTimeOffset ReservationDate) : IRequest<Result>;

public sealed class UpdateReservationCommandHandler(
    IReservationRepository reservationRepository,
    ITableService tableService,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ReservationBusinessRules reservationBusinessRules) : IRequestHandler<UpdateReservationCommand, Result>
{
    public async Task<Result> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        Reservation reservation = await reservationBusinessRules.EnsureReservationIdExists(request.Id, cancellationToken);

        bool doesExists = await tableService.EnsureTableIdExists(request.TableId, cancellationToken);

        if (doesExists)
            throw new NotFoundException(ReservationExceptionMessages.TableDoesNotExist);

        await reservationBusinessRules.EnsureNoDuplicateReservationForUpdate(request.Id, request.CustomerEmail, request.ReservationDate, request.TableId, cancellationToken);

        reservation = mapper.Map(request, reservation);

        reservationRepository.Update(reservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
