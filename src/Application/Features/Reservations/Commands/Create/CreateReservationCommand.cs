using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Models.Emails;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Contracts.Infrastructure.Services;
using Application.Features.Reservations.Constans;
using Application.Features.Tables.Service;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reservations.Commands.Create;

public sealed record CreateReservationCommand(
    string CustomerName,
    string CustomerPhone,
    string CustomerEmail,
    string SpecialRequest,
    int GuestCount,
    DateTimeOffset ReservationDate) : IRequest<CreatedReservationResponse>
{

    public sealed class CreateReservationCommandHandler(
        IReservationRepository reservationRepository,
        ITableService tableService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IEmailNotificationService emailNotificationService) : IRequestHandler<CreateReservationCommand, CreatedReservationResponse>
    {
        public async Task<CreatedReservationResponse> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var tables = await tableService.FindAvailableTablesForReservation(request.ReservationDate, request.GuestCount, cancellationToken);

            if (tables == null || tables.Count == 0)
                throw new NotFoundException(ReservationExceptionMessages.NoAvailableTables);

            var table = tables[0];

            Reservation reservation = mapper.Map<Reservation>(request);
            reservation.TableId = table.Id;

            await reservationRepository.AddAsync(reservation);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            ReservationEmail reservationEmail = new(reservation.CustomerEmail, reservation.CustomerName, table.Number, reservation.ReservationDate, reservation.GuestCount);
            await emailNotificationService.SendReservationEmailAsync(reservationEmail);

            CreatedReservationResponse response = new(reservation.Id, "Rezervasyon Başarıyla Yapıldı.");
            return response;
        }
    }

}
